const express = require('express');
const passengerRoutes = require('./routes/passengerRoutes');

const app = express();
app.use(express.json());

app.use('/passenger', passengerRoutes);

module.exports = app;
