"use strict";

// Importaciones necesarias para el funcionamiento del servidor
import express from "express";

import mysql from "mysql2/promise";

const app = express();
const port = 3100;

app.use(express.json());

// Funcion para conectarse a la base de datos
async function connectToDB() {
  return await mysql.createConnection({
    host: "localhost",
    user: "tc2005b",
    password: "asdf05",
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
      console.log("Connection closed succesfully!");
    }
  }
});

// Endpoint para obtener una Carta en específico por su ID
app.get("/api/awakening/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    // The ? character is used as a placeholder for the values that will be passed to the query. This is a security measure to avoid SQL injection attacks.
    const [results, fields] = await connection.execute(
      "select * from Cards where card_ID = ?",
      [request.params.id]
    );

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
      console.log("Connection closed succesfully!");
    }
  }
});

// Endpoint para agregar una Carta
app.post("/api/awakening/cards", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body instanceof Array ? request.body : [request.body];

    for (const card of data) {
      const [results, fields] = await connection.execute(
        "insert into Cards (card_name, card_description, attack, defense, healing, card_realm, power_cost, exp_cost, rarity, card_level, Effect_type) values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
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
      console.log(`${results.affectedRows} rows affected`);
      console.log(results);
    }

    response.status(200).json({ message: "Cards added successfully" });
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

// Endpoint para actualizar una Carta en específico por su ID
app.put("/api/awakening/cards/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;

    const [results, fields] = await connection.execute(
      "update Cards set card_name=?, card_description=?, attack=?, defense=?, healing=?, card_realm=?, power_cost=?, exp_cost=?, rarity=?, card_level=?, Effect_type=? where card=ID=?",
      [
        data.card_name,
        data.card_description,
        data.attack,
        data.defense,
        data.healing,
        data.card_realm,
        data.power_cost,
        data.exp_cost,
        data.rarity,
        data.card_level,
        data.Effect_type,
        request.params.id,
      ]
    );

    console.log(`${results.affectedRows} rows affected`);
    console.log(results);
    response.status(200).json({ message: "Card updated successfully" });
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
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
    response.status(200).json({
      message: `Data deleted correctly: ${results.affectedRows} rows deleted.`,
    });
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
    }
  }
});

app.post("/api/awakening/players", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    const data = request.body;

    // Verificar que el nombre no exista en la base de datos
    const [userExists] = await connection.execute(
      "select user_name from Player where user_name = ?",
      [data.user_name]
    );

    if (userExists.length > 0) {
      // Si el usuario ya existe, regresar un error
      return response
        .status(400)
        .json({
          message: "User name already exists, please choose another one",
        });
    }

    // Proceed to insert the new player since user_name does not exist
    const [results] = await connection.execute(
      "insert into Player (player_name, player_last_name, player_age, user_name, password, realm, is_npc, player_exp, win_record, lose_record, coins, tokens) values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
      [
        data.player_name,
        data.player_last_name,
        data.player_age,
        data.user_name,
        data.password,
        data.realm,
        data.is_npc,
        data.player_exp,
        data.win_record,
        data.lose_record,
        data.coins,
        data.tokens,
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

// Endpoint para obtener un jugador en específico por su ID
app.get("/api/awakening/players/:id", async (request, response) => {
  let connection = null;

  try {
    connection = await connectToDB();

    // The ? character is used as a placeholder for the values that will be passed to the query. This is a security measure to avoid SQL injection attacks.
    const [results, fields] = await connection.execute(
      "select * from Player where player_ID = ?",
      [request.params.id]
    );

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
      console.log("Connection closed succesfully!");
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
    response.status(200).json({
      message: `Data deleted correctly: ${results.affectedRows} rows deleted.`,
    });
  } catch (error) {
    response.status(500);
    response.json(error);
    console.log(error);
  } finally {
    if (connection !== null) {
      connection.end();
      console.log("Connection closed succesfully!");
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
      "select * from Player where user_name = ? and password = ?",
      [data.user_name, data.password]
    );

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
      console.log("Connection closed succesfully!");
    }
  }
});

// Inicialización del servidor
app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
