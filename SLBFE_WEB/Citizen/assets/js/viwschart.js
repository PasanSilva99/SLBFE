var dates = [19, 20, 21, 22, 23, 24, 25];
var views = [3, 0, 5, 9, 21, 5, 28];

new Chart("views-chart", {
    type: "line",
    data: {
      labels: dates,
      datasets: [{
        fill: false,
        backgroundColor: "rgba(255,255,255,1)",
        borderColor: "rgba(123, 97, 255,1)",
        data: views,
        tension: 0.5,
        borderWidth: 1
      }]
    },
    options: {
        plugins: {
            legend: {
                display: false
            }
        },
        scales: {
            y: {
                grid: {
                  color: 'rgb(203, 203, 203)'
                }
              },
              x: {
                grid: {
                  color: 'rgba(203, 203, 203,0)'
                }
              }
        }
    }
  });