/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
function random_color(alpha = 1.0) {
  const r_c = () => Math.round(Math.random() * 255);
  return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`;
}

Chart.defaults.font.size = 16;

async function Win_Lose() {
  const username =
    document.getElementById("username").value || "defaultUsername";
  const url = `http://localhost:3200/api/awakening/players/${encodeURIComponent(
    username
  )}/stats/win_lose`;

  try {
    const response = await fetch(url);
    if (!response.ok) throw new Error("Failed to fetch win/lose data");
    const data = await response.json();

    const ctx = document.getElementById("apiChart1").getContext("2d");
    if (window.winLoseChart) window.winLoseChart.destroy(); // Destroy existing chart if it exists
    window.winLoseChart = new Chart(ctx, {
      type: "pie",
      data: {
        labels: ["Win", "Lose"],
        datasets: [
          {
            label: "Win vs Lose",
            data: [data.win_record, data.lose_record],
            backgroundColor: [
              "rgba(54, 162, 235, 0.6)",
              "rgba(255, 99, 132, 0.6)",
            ],
            borderColor: ["rgba(54, 162, 235, 1)", "rgba(255, 99, 132, 1)"],
            borderWidth: 1,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: "top" },
          title: {
            display: true,
            text: `Win and Lose Record of Player ${encodeURIComponent(
              username
            )}`,
            font: {
              size: 20,
            },
          },
        },
      },
    });
  } catch (error) {
    console.error("Error fetching or displaying win/lose chart:", error);
  }
}

document.addEventListener("DOMContentLoaded", function () {
  document.getElementById("username").value = "john.doe"; // Set the default username
  const getDataButton = document.getElementById("getDataButton");
  if (getDataButton) {
    getDataButton.addEventListener("click", Win_Lose);
  } else {
    console.error("Data button not found");
  }
  Win_Lose(); // Optionally run on load to display default data
});

try {
  const response = await fetch(
    "http://localhost:3200/api/awakening/players/stats/ages"
  );
  if (!response.ok) throw new Error("Failed to fetch age data");
  const data = await response.json();

  const ctx = document.getElementById("apiChart2").getContext("2d");
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: data.map((item) => item.player_age.toString()),
      datasets: [
        {
          label: "Count of Players by Age",
          data: data.map((item) => item.count_of_people),
          backgroundColor: data.map(() => random_color(0.8)),
        },
      ],
    },
    options: {
      scales: { y: { beginAtZero: true } },
      responsive: true,
      plugins: {
        legend: { display: false },
        title: {
          display: true,
          text: "Distribution of Players by Age",
          font: {
            size: 20,
          },
        },
      },
    },
  });
} catch (error) {
  console.error("Error fetching or displaying age chart:", error);
}

try {
  const response = await fetch(
    "http://localhost:3200/api/awakening/players/stats/levels"
  );
  if (!response.ok) throw new Error("Failed to fetch level data");
  const data = await response.json();

  const ctx = document.getElementById("apiChart3").getContext("2d");
  new Chart(ctx, {
    type: "line",
    data: {
      labels: data.map((item) => `Level ${item.level}`),
      datasets: [
        {
          label: "Count of Players by Level",
          data: data.map((item) => item.count_of_people),
          backgroundColor: data.map(() => random_color(0.8)),
          tension: 0.1,
          pointRadius: 10,
        },
      ],
    },
    options: {
      scales: { y: { beginAtZero: true } },
      responsive: true,
      plugins: {
        legend: { display: false },
        title: {
          display: true,
          text: "Player Count by Level",
          font: {
            size: 20,
          },
        },
      },
    },
  });
} catch (error) {
  console.error("Error fetching or displaying level chart:", error);
}

try {
  const response = await fetch(
    "http://localhost:3200/api/awakening/players/stats/used_cards"
  );
  if (!response.ok) throw new Error("Failed to fetch used cards data");
  const data = await response.json();

  const ctx = document.getElementById("apiChart4").getContext("2d");
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: data.map((card) => card.card_name),
      datasets: [
        {
          label: "Usage Count of Cards",
          data: data.map((card) => card.usage_count),
          backgroundColor: data.map(() => random_color(0.8)),
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      responsive: true,
      plugins: {
        legend: {
          display: false,
        },
        title: {
          display: true,
          text: "Top 10 Most Used Cards",
          font: {
            size: 20,
          },
        },
      },
    },
  });
} catch (error) {
  console.error("Error fetching or displaying most used cards chart:", error);
}
