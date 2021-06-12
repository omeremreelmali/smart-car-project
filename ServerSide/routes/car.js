const Router = require('express').Router();
const connection = require('../config/connection');

Router.get('/:id', (req, res) => {
    const id = req.params.id;
    connection.query(
        "call getCar(?)", [id], (err, results) => {
            if (err) {
                const success = false;

                res.status(500).json({ success, err });
            }
            const success = true;
            const data = results[0];
            res.status(200).json({ success, data })
        }
    )
});


Router.post('/', (req, res) => {
    const { plate, user_id, brand, city_code, color, model, engine_model } = req.body;
    console.log(req.body)
    const sql = "insert into vehicles(plate, user_id, brand, city_code, color, model, engine_model)values(?,?,?,?,?,?,?)";
    connection.query(
        sql, [plate, user_id, brand, city_code, color, model, engine_model],
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

Router.delete('/:id', (req, res) => {
    const id = req.params.id;
    const sql = "Delete from vehicles where id=?";
    connection.query(
        sql, [id],
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
})


module.exports = Router;