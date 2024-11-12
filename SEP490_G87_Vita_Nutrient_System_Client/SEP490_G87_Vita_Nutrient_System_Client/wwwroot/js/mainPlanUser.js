var ctx = document.getElementById("myChart").getContext("2d");
var myPieChart = new Chart(ctx, {
  type: "pie",
  data: {
    labels: ["Fat", "Protein", "Carbs"],
    datasets: [
      {
        label: "Macronutrient Breakdown",
        data: [39, 27, 34],
        backgroundColor: [
          "rgba(255, 99, 132, 0.6)",
          "rgba(54, 162, 235, 0.6)",
          "rgba(255, 206, 86, 0.6)",
        ],
        borderColor: [
          "rgba(255, 99, 132, 1)",
          "rgba(54, 162, 235, 1)",
          "rgba(255, 206, 86, 1)",
        ],
        borderWidth: 1,
      },
    ],
  },
  options: {
    responsive: false, 
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: "top",
      },
      tooltip: {
        enabled: true,
      },
    },
  },
});
