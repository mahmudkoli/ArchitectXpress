const amqp = require('amqplib');
require('dotenv').config();

const RABBITMQ_SETTINGS = {
    host: process.env.RABBITMQ_HOST || 'localhost',
    port: process.env.RABBITMQ_PORT || 5672,
    username: process.env.RABBITMQ_USER || 'guest',
    password: process.env.RABBITMQ_PASS || 'guest',
    exchangeName: process.env.RABBITMQ_EXCHANGE || 'passenger_registration_exchange'
};

async function connectRabbitMQ() {
    const connectionString = `amqp://${RABBITMQ_SETTINGS.username}:${RABBITMQ_SETTINGS.password}@${RABBITMQ_SETTINGS.host}:${RABBITMQ_SETTINGS.port}`;
    const connection = await amqp.connect(connectionString);
    const channel = await connection.createChannel();
    await channel.assertExchange(RABBITMQ_SETTINGS.exchangeName, 'fanout', { durable: true });
    return { connection, channel };
}

module.exports = { connectRabbitMQ, RABBITMQ_SETTINGS };
