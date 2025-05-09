import { Line } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip } from 'chart.js';

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip
);

interface LineChartProps {
    data: { name: string; value: number }[];
}

const LineChart = ({ data }: LineChartProps) => {
    const options = {
        responsive: true,
        maintainAspectRatio: false
    };
    const chartData = {
        labels: data.map((item) => item.name),
        datasets: [
            {
                label: 'Sales',
                data: data.map((item) => item.value),
                fill: false,
                borderColor: '#8884d8',
                tension: 0.1,
            },
        ],
    };

    return (
        <Line data={chartData} options={options}/>
    );
};

export default LineChart;
