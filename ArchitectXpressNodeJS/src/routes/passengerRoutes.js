const express = require('express');
const Passenger = require('../models/Passenger');
const { publishMessage } = require('../services/publisher');

const app = express();

// Middleware to parse JSON request bodies (built-in since Express 4.16.0)
app.use(express.json());

const router = express.Router();

router.post('/publish', async (req, res) => {
    try {
        console.log('Request body:', req.body);
        const passenger = new Passenger(req.body);
        console.log('Passenger:', passenger);
        await publishMessage(passenger);

        res.status(200).json({ message: 'Passenger data published successfully!' });

    } catch (error) {
        console.error('Error publishing message:', error);
        res.status(500).json({ error: 'Failed to publish message' });
    }
});

module.exports = router;
