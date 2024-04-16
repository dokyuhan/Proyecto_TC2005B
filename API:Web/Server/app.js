"use strict";

// Importaciones necesarias para el funcionamiento del servidor
import express from "express";

import mysql from "mysql2/promise";

const app = express();
const port = 3200;

app.use(express.json());

// Funcion para conectarse a la base de datos
/*
async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "Awakening",
    password: "qwer1234",
    database: "Awakening_realm",
  });
}
*/
async function connectToDB() {
  return await mysql.createConnection({
    host: "127.0.0.1",
    user: "root",
    password: "@Dokyu2379",
    database: "Awakening_realm",
  });
}

// Endpoint para obtener todas las Cartas
app.get("/api/awakening/cards", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute("select * from Cards");

    console.log(`${results.length} rows returned`);
    console.log(results);
    response.status(200).json(results);
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para obtener una Carta en específico por su ID
app.get("/api/awakening/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "select * from Cards where card_ID = ?",
      [request.params.id]
    );

    console.log(`${results.length} rows returned`);
    console.log(results);

    if (results.length === 0) {
      response.status(404).json({ message: "Card not found" });
    } else {
      response.status(200).json(results[0]);
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para agregar una Carta
app.post("/api/awakening/cards", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body instanceof Array ? request.body : [request.body];

    const requiredFields = [
      "card_name",
      "card_description",
      "attack",
      "defense",
      "healing",
      "card_realm",
      "power_cost",
      "exp_cost",
      "rarity",
      "card_level",
      "Effect_type",
    ];

    for (const card of data) {
      const missingFields = requiredFields.filter(
        (field) => card[field] === undefined || card[field] === ""
      );

      if (missingFields.length > 0) {
        return response.status(400).json({
          message:
            "Missing required card information: " + missingFields.join(", "),
        });
      }

      // Verificación de existencia del nombre de la carta
      const [existingCard] = await connection.execute(
        "SELECT card_name FROM Cards WHERE card_name = ?",
        [card.card_name]
      );
      if (existingCard.length > 0) {
        return response.status(409).json({
          message: `A card with the name "${card.card_name}" already exists. Please choose a different name.`,
        });
      }

      // Si Effect_type no es null, verifica que exista en la tabla Effect
      if (card.Effect_type !== null) {
        const [existingEffect] = await connection.execute(
          "SELECT Effect_type FROM Effect WHERE Effect_type = ?",
          [card.Effect_type]
        );
        if (existingEffect.length === 0) {
          return response.status(400).json({
            message: `Effect_type '${card.Effect_type}' does not exist.`,
          });
        }
      }

      // Inserción de la carta
      const [insertResult] = await connection.execute(
        "INSERT INTO Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, exp_cost, rarity, card_level, Effect_type) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
        [
          card.card_name,
          card.card_description,
          card.attack,
          card.defense,
          card.healing,
          card.card_realm,
          card.power_cost,
          card.exp_cost,
          card.rarity,
          card.card_level,
          card.Effect_type,
        ]
      );
      console.log(`${insertResult.affectedRows} rows affected`);
    }

    response.status(200).json({ message: "Cards added successfully" });
  } catch (error) {
    console.log(error);
    response
      .status(500)
      .json({ message: "An error occurred while adding cards.", error });
  } finally {
    if (connection) {
      connection.end();
    }
  }
});

// Endpoint para actualizar una Carta en específico por su ID
app.put("/api/awakening/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;
    const cardId = request.params.id;

    const allowedFields = [
      "card_name",
      "card_description",
      "attack",
      "defense",
      "healing",
      "card_realm",
      "power_cost",
      "exp_cost",
      "rarity",
      "card_level",
      "Effect_type",
    ];

    let setClause = [];
    let queryParams = [];
    for (const field of allowedFields) {
      if (data.hasOwnProperty(field)) {
        setClause.push(`${field} = ?`);
        queryParams.push(data[field]);
      }
    }

    // Si no hay campos válidos para actualizar, devolver un error
    if (setClause.length === 0) {
      return response
        .status(400)
        .json({ message: "No valid fields provided for update." });
    }

    queryParams.push(cardId);

    const query = `UPDATE Cards SET ${setClause.join(", ")} WHERE card_ID = ?`;

    // Ejecutar la consulta de actualización
    const [results] = await connection.execute(query, queryParams);

    if (results.affectedRows === 0) {
      return response
        .status(404)
        .json({ message: "Card not found or no new data provided." });
    }

    response.status(200).json({ message: "Card updated successfully" });
  } catch (error) {
    console.log(error);
    response
      .status(500)
      .json({ message: "An error occurred while updating the card.", error });
  } finally {
    if (connection) {
      connection.end();
    }
  }
});

// Endpoint para eliminar una Carta en específico por su ID
app.delete("/api/awakening/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "delete from Cards where card_ID = ?",
      [request.params.id]
    );

    console.log(`${results.affectedRows} rows affected`);
    console.log(results);

    if (results.affectedRows === 0) {
      response.status(404).json({ message: "Card not found" });
    } else {
      response.status(200).json({
        message: `Data deleted correctly: ${results.affectedRows} rows deleted.`,
      });
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para crear un jugador nuevo
app.post("/api/awakening/players", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;
    const requiredFields = [
      "player_name",
      "player_last_name",
      "player_age",
      "user_name",
      "password",
      "realm",
      "is_npc",
      "level",
      "player_exp",
      "win_record",
      "lose_record",
      "coins",
      "token",
    ];

    const missingFields = requiredFields.filter(
      (field) => data[field] === undefined || data[field] === null
    );

    if (missingFields.length > 0) {
      return response.status(400).json({
        message:
          "Missing required user information: " + missingFields.join(", "),
      });
    }

    const [userExists] = await connection.execute(
      "select user_name from Players where user_name = ?",
      [data.user_name]
    );

    if (userExists.length > 0) {
      return response.status(400).json({
        message: "User name already exists, please choose another one",
      });
    }

    const [results] = await connection.execute(
      "insert into Players (player_name, player_last_name, player_age, user_name, password, realm, is_npc, level, player_exp, win_record, lose_record, coins, token) values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
      [
        data.player_name,
        data.player_last_name,
        data.player_age,
        data.user_name,
        data.password,
        data.realm,
        data.is_npc,
        data.level,
        data.player_exp,
        data.win_record,
        data.lose_record,
        data.coins,
        data.token,
      ]
    );
    console.log(`${results.affectedRows} rows affected`);
    response.status(200).json({ message: "Player added successfully" });
  } catch (error) {
    console.log(error);
    response.status(500).json(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para iniciar sesión
app.post("/api/awakening/players/login", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;

    const [results, fields] = await connection.execute(
      "SELECT * FROM Players WHERE user_name = ? AND password = ?",
      [data.user_name, data.password]
    );

    console.log(`${results.length} rows returned`);
    console.log(results);

    if (results.length > 0) {
      response.status(200).json(results[0]);
    } else {
      response.status(404).json({ message: "Incorrect username or password" });
    }
  } catch (error) {
    console.log(error);
    response.status(500).json(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para obtener un jugador en específico por su ID
app.get("/api/awakening/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    // The ? character is used as a placeholder for the values that will be passed to the query. This is a security measure to avoid SQL injection attacks.
    const [results, fields] = await connection.execute(
      "select * from Players where player_ID = ?",
      [request.params.id]
    );

    console.log(`${results.length} rows returned`);
    console.log(results);

    if (results.length === 0) {
      response.status(404).json({ message: "Player not found" });
    } else {
      response.status(200).json(results[0]);
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para eliminar un Jugador en específico por su ID
app.delete("/api/awakening/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "delete from Players where player_ID = ?",
      [request.params.id]
    );

    console.log(`${results.affectedRows} rows affected`);
    console.log(results);

    if (results.affectedRows === 0) {
      response.status(404).json({ message: "Player not found" });
    } else {
      response.status(200).json({
        message: `Player deleted correctly: ${results.affectedRows} rows deleted.`,
      });
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para obtener los stats de un usuario en específico por su ID
app.get("/api/awakening/players/:id/stats", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "select win_record, lose_record from Players where player_ID = ?",
      [request.params.id]
    );

    console.log(`${results.length} rows returned`);
    console.log(results);

    if (results.length === 0) {
      response.status(404).json({ message: "Player not found" });
    } else {
      response.status(200).json(results[0]);
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para crear un inventario y/o añadir cartas al inventario de un jugador
app.post("/api/awakening/players/inventory", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();
    const { card_IDs, player_ID, deck_ID } = request.body;

    if (!card_IDs || !player_ID || !deck_ID) {
      return response.status(400).json({
        message:
          "Missing required fields. Ensure card_IDs, player_ID, and deck_ID are provided.",
      });
    }

    if (!Array.isArray(card_IDs) || card_IDs.length === 0) {
      return response.status(400).json({
        message: "card_IDs must be a non-empty array.",
      });
    }

    let existingIDs = [];
    for (const card_ID of card_IDs) {
      const [results] = await connection.execute(
        "SELECT card_ID FROM Inventory WHERE card_ID = ? AND player_ID = ?",
        [card_ID, player_ID]
      );
      if (results.length > 0) {
        existingIDs.push(card_ID);
      }
    }

    if (existingIDs.length > 0) {
      return response.status(400).json({
        message:
          "Some card IDs are already in the inventory for the given player.",
        existingIDs: existingIDs,
      });
    }

    let insertedRows = 0;
    for (const card_ID of card_IDs) {
      const [result] = await connection.execute(
        "INSERT INTO Inventory (card_ID, player_ID, deck_ID) VALUES (?, ?, ?)",
        [card_ID, player_ID, deck_ID]
      );
      insertedRows += result.affectedRows;
    }

    console.log(`${insertedRows} rows affected`);
    response.status(200).json({ message: "Inventory added successfully" });
  } catch (error) {
    console.error(error);
    response
      .status(500)
      .json({ message: "Internal server error", error: error.message });
  } finally {
    if (connection) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para obtener un inventario en específico por el id de jugador
app.get("/api/awakening/players/:id/inventory", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const [results, fields] = await connection.execute(
      "SELECT card_ID FROM Inventory WHERE player_ID = ?",
      [request.params.id]
    );

    console.log(`${results.length} rows returned`);
    console.log(results);

    if (results.length === 0) {
      response.status(404).json({ message: "Card not found" });
    } else {
      const cardIds = results.map((row) => row.card_ID);

      response.status(200).json({ cardIds });
    }
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para mandar los datos del mazo
app.post("/api/awakening/players/inventory/deck", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const cards = request.body.cards; // Asume que 'cards' es un arreglo de objetos
    let insertQuery =
      "INSERT INTO Inventory (card_ID, player_ID, deck_ID) VALUES ?";
    let values = cards.map((card) => [
      card.card_ID,
      card.player_ID,
      card.deck_ID,
    ]);

    const [results] = await connection.query(insertQuery, [values]);

    console.log(`${results.affectedRows} rows inserted`);
    response
      .status(200)
      .json({ message: `${results.affectedRows} cards added successfully` });
  } catch (error) {
    console.error("Error inserting into Inventory:", error);
    response.status(500).json({ error: "Internal server error" });
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para crear una partida
app.post("/api/awakening/match/create", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body.game;

    const requiredFields = [
      "player_ID_1",
      "player_ID_2",
      "winner_ID",
      "game_level",
      "game_scene",
      "game_duration",
      "game_turns",
    ];

    const missingFields = requiredFields.filter(
      (field) => data[field] === undefined
    );

    if (missingFields.length > 0) {
      return response.status(400).json({
        error: "Missing required information",
        missingFields,
      });
    }

    const [results] = await connection.execute(
      "INSERT INTO Game (player_ID_1, player_ID_2, winner_ID, game_level, game_scene, game_duration, game_turns) VALUES (?, ?, ?, ?, ?, ?, ?)",
      [
        data.player_ID_1,
        data.player_ID_2,
        data.winner_ID,
        data.game_level,
        data.game_scene,
        data.game_duration,
        data.game_turns,
      ]
    );

    console.log(`${results.affectedRows} rows inserted`);
    response
      .status(200)
      .json({ message: `${results.affectedRows} game added successfully` });
  } catch (error) {
    console.error("Error inserting into Game:", error);
    response.status(500).json({ error: "Internal server error" });
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed successfully!");
    }
  }
});

// Endpoint para comprar una carta con monedas (hace todas las verificaciones necesarias)
app.post(
  "/api/awakening/players/:id/inventory/buyCard",
  async (request, response) => {
    let connection = null;

    try {
      connection = await connectToDB();

      if (!request.params.id) {
        return response.status(400).json({
          message: "Missing required field. Ensure player_ID is provided.",
        });
      }

      const playerID = request.params.id;
      const cardPrice = 150;

      const [player] = await connection.execute(
        "SELECT coins FROM Players WHERE player_ID = ?",
        [playerID]
      );

      if (player.length === 0) {
        return response.status(404).json({ message: "Player not found" });
      }

      if (player[0].coins < cardPrice) {
        return response
          .status(400)
          .json({ message: "Not enough coins to buy the card." });
      }

      let attempts = 0;
      let cardFetched;
      let allCardsOwned = true;

      do {
        const randomNumber = Math.floor(Math.random() * 40) + 1;

        [cardFetched] = await connection.execute(
          "SELECT * FROM Cards WHERE card_ID = ?",
          [randomNumber]
        );

        const [existingCard] = await connection.execute(
          "SELECT card_ID FROM Inventory WHERE card_ID = ? AND player_ID = ?",
          [randomNumber, playerID]
        );

        attempts++;

        if (existingCard.length === 0 && cardFetched.length > 0) {
          allCardsOwned = false;
          break;
        }

        if (attempts >= 40) {
          break;
        }
      } while (true);

      if (allCardsOwned) {
        return response
          .status(409)
          .json({ message: "All cards have already been purchased!" });
      }

      await connection.beginTransaction();

      const [insertResult] = await connection.execute(
        "INSERT INTO Inventory (card_ID, player_ID) VALUES (?, ?)",
        [cardFetched[0].card_ID, playerID]
      );
      if (insertResult.affectedRows === 0) {
        await connection.rollback();
        return response
          .status(500)
          .json({ message: "Failed to insert card into inventory." });
      }

      const [updateResult] = await connection.execute(
        "UPDATE Players SET coins = coins - ? WHERE player_ID = ?",
        [cardPrice, playerID]
      );
      const [remainingCoins] = await connection.execute(
        "SELECT coins FROM Players WHERE player_ID = ?",
        [playerID]
      );
      if (updateResult.affectedRows === 0) {
        await connection.rollback();
        return response
          .status(500)
          .json({ message: "Failed to update player coins." });
      }

      await connection.commit();
      response.status(200).json({
        message: "Card purchased!",
        card: cardFetched[0],
        coins: remainingCoins[0],
      });
    } catch (error) {
      console.error("Error in buying card:", error);
      await connection.rollback();
      response.status(500).json({ message: "Internal server error", error });
    } finally {
      if (connection !== null) {
        connection.end();
      }
    }
  }
);

// Endpoint para obtener las monedas de un jugador en específico por su ID
app.get(
  "/api/awakening/players/:id/inventory/coins",
  async (request, response) => {
    let connection = null;

    try {
      connection = await connectToDB();

      const [results, fields] = await connection.execute(
        "SELECT coins FROM Players WHERE player_ID = ?",
        [request.params.id]
      );

      console.log(`${results.length} rows returned`);
      console.log(results);

      if (results.length === 0) {
        response.status(404).json({ message: "Player not found" });
      } else {
        response.status(200).json(results[0]);
      }
    } catch (error) {
      response.status(500);
      response.json(error);
      console.log(error);
    } finally {
      if (connection !== null) {
        connection.end();
        console.log("Connection closed successfully!");
      }
    }
  }
);

// Manejo de errores genérico
app.use((err, request, response, next) => {
  console.error(err); // Para propósitos de depuración
  response.status(500).json({ message: "Something went wrong" });
});

// Inicialización del servidor
app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
