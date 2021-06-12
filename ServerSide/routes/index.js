const Router = require("express").Router();
const User = require('./user');
const Car = require('./car');
const Account = require('./account');
const Map = require('./map');
const Vforms = require('./vforms');
const Cardetail = require('./cardetail');

Router.use('/vforms', Vforms);
Router.use('/user', User);
Router.use('/car', Car);
Router.use('/account', Account);
Router.use('/map', Map);
Router.use('/cardetail', Cardetail);

module.exports = Router;