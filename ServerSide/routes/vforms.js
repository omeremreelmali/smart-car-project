const Router = require('express').Router();
const connection = require('../config/connection');




Router.post('/vehicleadd', (req, res) => {
    const { plate, color, fuel, userid } = req.body;
    console.log(req.body)
    const sql = "insert into newvehicleform(plate, color, fuel, userid)values(?,?,?,?)";
    connection.query(
        sql, [plate, color, fuel, userid],
        (err, result) => {
            if (err) {
                const success = false;
                res.status(503).json({ success, err });
            } else {
                const success = true;
                res.status(200).json({ success });
            }
        }
    )
});
Router.post('/requestform', (req, res) => {
    const { subject, email, message, userid } = req.body;
    console.log(req.body)
    const sql = "insert into requestform(subject, email, message, userid)values(?,?,?,?)";
    connection.query(
        sql, [subject, email, message, userid],
        (err, result) => {
            if (err) {
                const success = false;
                res.status(503).json({ success, err });
            } else {
                const success = true;
                res.status(200).json({ success });
            }
        }
    )
});
Router.post('/vehicleupdate', (req, res) => {
    const { plate, newplate, fuel, color, userid } = req.body;
    console.log(req.body)
    const sql = "insert into vehicleupdateforms(plate, newplate, fuel, color, userid)values(?,?,?,?,?)";
    connection.query(
        sql, [plate, newplate, fuel, color, userid],
        (err, result) => {
            if (err) {
                const success = false;
                res.status(503).json({ success, err });
            } else {
                const success = true;
                res.status(200).json({ success });
            }
        }
    )
});




module.exports = Router;