const Router = require('express').Router();
const connection = require('../config/connection');

Router.get("/:id", (req, res) => {
  const id = req.params.id;
  connection.query(
      "SELECT brands.brand_name,vehicle_locations.enlem,vehicle_locations.boylam,vehicle_locations.speed from vehicle_locations join vehicles on vehicle_locations.vehicle_id = vehicles.id join brands on brands.id = vehicles.brand where vehicle_id=?", [id],
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
Router.get("/", (req, res) => {
    const id = req.params.id;
    connection.query(
        "SELECT brands.brand_name,vehicle_locations.enlem, vehicle_locations.boylam,vehicle_locations.speed from vehicle_locations join vehicles on vehicle_locations.vehicle_id = vehicles.id join brands on brands.id = vehicles.brand ",
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

module.exports = Router;