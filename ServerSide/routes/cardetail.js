const Router = require("express").Router();
const connection = require("../config/connection");




Router.get("/:id", (req, res) => {
    const id = req.params.id;
    connection.query(
        "SELECT vehicle_id, enlem, boylam, speed, vehicle_state, stop_state from vehicle_locations  where 	vehicle_id =? ", [id],
        function(error, results) {
            if (error) console.log(error);
            if (!results) {
                const success = false;
                res.status(404).json({ success });
            } else {
                const success = true;
                const data = results;
                res.status(200).json({ success, data });
            }
            console.log(results);
        }
    );
  });


Router.post("/", (req, res) => {
    const { id } = req.body;
    connection.query(
        "SELECT vehicle_id, enlem, boylam, speed, vehicle_state, stop_state from vehicle_locations  where 	vehicle_id =? ", [id],
        function(error, results) {
            if (error) console.log(error);
            if (!results[0]) {
                const success = false;
                res.status(404).json({ success });
            } else {
                const success = true;
                const data = results;
                res.status(200).json({ success, data });
            }
            console.log(results);
        }
    );
});


Router.post('/stopcar', (req, res) => {
    const { id, stop_state } = req.body;
    const sql = "update vehicle_locations set stop_state=? where vehicle_id=?";
    connection.query(
        sql, [stop_state, id],
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
Router.post('/crashReset', (req, res) => {
    const {stop_state } = req.body;
    const sql = "update vehicle_locations set stop_state=? , vehicle_state=0 ,speed=0 where vehicle_id=18";
    connection.query(
        sql, [stop_state],
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

Router.post('/crash', (req, res) => {
    const {stop_state } = req.body;
    const sql = "update vehicle_locations set stop_state=? , vehicle_state=1 ,speed=0 where vehicle_id=18";
    connection.query(
        sql, [stop_state],
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

Router.post('/simulation', (req, res) => {
    const { speed, stop_state } = req.body;
    const sql = "update vehicle_locations set stop_state=?, speed=? where vehicle_id=18";
    connection.query(
        sql, [stop_state,speed],
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