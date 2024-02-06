import { useState, useEffect, Fragment } from "react";
import Sheet from "@mui/joy/Sheet";
import Modal from "@mui/joy/Modal";
import ModalClose from "@mui/joy/ModalClose";
import Button from "@mui/joy/Button";

import styles from "./Weather.module.css";

const api = {
  key: "c23ad1b9d46f16263053c161aa3bf09e",
  base: "https://api.openweathermap.org/data/2.5/",
};

function Weather(props) {
  const [isOpen, setIsOpen] = useState(false);
  const [weather, setWeather] = useState({});
  let lat = 1;
  let lon = 1;

  function weatherClickHandler() {
    setIsOpen(true);
  }

  if (props.enteredWayPoints) {
    lat = props.enteredWayPoints.lat;
    lon = props.enteredWayPoints.lng;
  }

  useEffect(() => {
    fetch(
      `https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${lon}&units=metric&appid=${api.key}`
    )
      .then((res) => res.json())
      .then((result) => {
        setWeather(result);
      });
  }, [lat, lon]);

  return (
    <Fragment>
      <Button
        color="primary"
        variant="soft"
        sx={{ width: "100%"}}
        onClick={weatherClickHandler}
      >
        Check weather
      </Button>

      {props.isDestinantion && (
        <Modal
          open={isOpen}
          onClose={() => setIsOpen(false)}
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <Sheet>
            <ModalClose
              variant="outlined"
              sx={{
                top: "calc(-1/4 * var(--IconButton-size))",
                right: "calc(-1/4 * var(--IconButton-size))",
                boxShadow: "0 2px 12px 0 rgba(0 0 0 / 0.2)",
                borderRadius: "50%",
                bgcolor: "white",
              }}
            />
            {weather.main && (
              <div className={styles["weather"]}>
                <div className={styles["top"]}>
                  <div>
                    <p className={styles["city"]}>
                      {weather.name}, {weather.sys.country}
                    </p>
                    <p className={styles["weather-description"]}>
                      {weather.weather[0].description}
                    </p>
                  </div>
                  <img
                    alt="weather"
                    className="weather-icon"
                    src={`icons/${weather.weather[0].icon}.png`}
                  />
                </div>
                <div className={styles["bottom"]}>
                  <p className={styles["temperature"]}>
                    {Math.round(weather.main.temp)}°C
                  </p>
                  <div className={styles["details"]}>
                    <div className={styles["parameter-row"]}>
                      <span className={styles["parameter-label"]}>Details</span>
                    </div>
                    <div className={styles["parameter-row"]}>
                      <span className={styles["parameter-label"]}>
                        Feels like
                      </span>
                      <span className={styles["parameter-value"]}>
                        {Math.round(weather.main.feels_like)}°C
                      </span>
                    </div>
                    <div className={styles["parameter-row"]}>
                      <span className={styles["parameter-label"]}>Wind</span>
                      <span className={styles["parameter-value"]}>
                        {weather.wind.speed} m/s
                      </span>
                    </div>
                    <div className={styles["parameter-row"]}>
                      <span className={styles["parameter-label"]}>
                        Humidity
                      </span>
                      <span className={styles["parameter-value"]}>
                        {weather.main.humidity}%
                      </span>
                    </div>
                    <div className={styles["parameter-row"]}>
                      <span className={styles["parameter-label"]}>
                        Pressure
                      </span>
                      <span className={styles["parameter-value"]}>
                        {weather.main.pressure} hPa
                      </span>
                    </div>
                  </div>
                </div>
              </div>
            )}
          </Sheet>
        </Modal>
      )}
    </Fragment>
  );
}

export default Weather;
