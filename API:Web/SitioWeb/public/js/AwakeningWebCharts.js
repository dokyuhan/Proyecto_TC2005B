/**
 * @param {number} alpha Indicated the transparency of the color
 * @returns {string} A string of the form 'rgba(240, 50, 123, 1.0)' that represents a color
 */
function random_color(alpha = 1.0) {
  const r_c = () => Math.round(Math.random() * 255);
  return `rgba(${r_c()}, ${r_c()}, ${r_c()}, ${alpha}`;
}

Chart.defaults.font.size = 16;

// We obtain a reference to the canvas that we are going to use to plot the chart.
const ctx = document.getElementById("firstChart").getContext("2d");

// To plot a chart, we need a configuration object that has all the information that the chart needs.
const firstChart = new Chart(ctx, {
  type: "bar",
  data: {
    labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
    datasets: [
      {
        label: "# of Votes",
        data: [12, 19, 3, 5, 2, 3],
        backgroundColor: [
          "rgba(255, 99, 132, 0.2)",
          "rgba(54, 162, 235, 0.2)",
          "rgba(255, 206, 86, 0.2)",
          "rgba(75, 192, 192, 0.2)",
          "rgba(153, 102, 255, 0.2)",
          "rgba(255, 159, 64, 0.2)",
        ],
        borderColor: [
          "rgba(255, 99, 132, 1)",
          "rgba(54, 162, 235, 1)",
          "rgba(255, 206, 86, 1)",
          "rgba(75, 192, 192, 1)",
          "rgba(153, 102, 255, 1)",
          "rgba(255, 159, 64, 1)",
        ],
        borderWidth: 1,
      },
    ],
  },
  options: {
    scales: {
      y: {
        beginAtZero: true,
      },
    },
  },
});

Chart.defaults.font.size = 16;

// Initialize the Win/Lose Chart
try {
  const response = await fetch(
    "http://localhost:3200/api/awakening/players/1/stats/win_lose"
  );
  if (!response.ok) throw new Error("Failed to fetch win/lose data");
  const data = await response.json();

  const ctx = document.getElementById("apiChart1").getContext("2d");
  const win_lose = new Chart(ctx, {
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
      },
    },
  });
} catch (error) {
  console.error("Error fetching or displaying win/lose chart:", error);
}

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
          backgroundColor: "rgba(153, 102, 255, 0.6)",
          borderColor: "rgba(153, 102, 255, 1)",
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: { y: { beginAtZero: true } },
      responsive: true,
      plugins: {
        legend: { position: "top" },
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
          backgroundColor: "rgba(75, 192, 192, 0.2)",
          borderColor: "rgba(75, 192, 192, 1)",
          borderWidth: 2,
          tension: 0.1,
        },
      ],
    },
    options: {
      scales: { y: { beginAtZero: true } },
      responsive: true,
      plugins: {
        legend: { position: "top" },
      },
    },
  });
} catch (error) {
  console.error("Error fetching or displaying level chart:", error);
}
