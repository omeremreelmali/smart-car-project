const Router = require("express").Router();
const connection = require("../config/connection");

Router.post("/", (req, res) => {
    const { tc, code } = req.body;
    connection.query(
        "SELECT users.id, users.role, users.name from users  where users.tc =? and users.code = ?", [tc, code],
        function(error, results) {
            if (error) console.log(error);
            if (!results[0]) {
                const success = false;
                res.status(404).json({ success });
            } else {
                const success = true;
                /*const user = {
                    id: results[0].id,
                    role: results[0].role,
                    name: results[0].name
                };*/
                const data = results;
                res.status(200).json({ success, data });
            }
            console.log(results);
        }
    );
});

Router.post("/userget", (req, res) => {
    const { id } = req.body;
    connection.query(
        "SELECT users.tc , users.phone , users.role, users.name, users.surname,users.email from users  where users.id =? ", [id],
        function(error, results) {
            if (error) console.log(error);
            if (!results[0]) {
                const success = false;
                res.status(404).json({ success });
            } else {
                const success = true;
                /*const user = {
                    id: results[0].id,
                    role: results[0].role,
                    name: results[0].name
                };*/
                const data = results;
                res.status(200).json({ success, data });
            }
            console.log(results);
        }
    );
});

Router.post('/register', (req, res) => {
    const { tc, name, surname, email, code } = req.body;
    console.log(req.body)
    const sql = "insert into users(tc, email, name, surname,code)values(?,?,?,?,?)";
    connection.query(
        sql, [tc, email, name, surname, code],
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
Router.put('/', (req, res) => {
    const { id, name, tc, surname, email, code } = req.body;
    const sql = "update users set email=?,tc=?, name=?, surname=?, code=? where id=?";
    connection.query(
        sql, [email, tc, name, surname, code, id],
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


Router.delete('/:id', (req, res) => {
    const id = req.params.id;
    const sql = "delete from users where users.id=?";
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