const express = require('express');
const cors = require('cors');
const app = express();
const connection = require('./config/connection');
const logger = require('morgan');
app.use(cors());
app.use(express.json());
app.use(logger("dev"))
app.use('/api', require('./routes'));
app.listen(4500, () => { console.log("server is up and runing") })