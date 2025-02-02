const { connectRabbitMQ, RABBITMQ_SETTINGS } = require('../config/rabbitmq');

async function publishMessage(message) {
    try {
        const { channel } = await connectRabbitMQ();
        channel.publish(RABBITMQ_SETTINGS.exchangeName, '', Buffer.from(JSON.stringify(message)));
        console.log('Message published:', message);
    } catch (error) {
        console.error('Failed to publish message:', error);
    }
}

module.exports = { publishMessage };
