const Router = require("express").Router();
const connection = require("../config/connection");

Router.get("/:id", (req, res) => {
    const id = req.params.id;
    connection.query(
        "call grage(?)", [id],
        (err, results) => {
            if (err) console.log(err);
            if (!results) {
                const success = false;
                res.status(404).json({ success });
            }
            const success = true;
            const data = results[0];
            res.status(200).json({ success, data });
        }
    );
});

module.exports = Router;