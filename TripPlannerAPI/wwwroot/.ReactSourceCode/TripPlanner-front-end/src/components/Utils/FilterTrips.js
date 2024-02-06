import { useState } from "react";
import FormControl from "@mui/joy/FormControl";
import Input from "@mui/joy/Input";
import Typography from "@mui/joy/Typography";
import Button from "@mui/joy/Button";
import Select from "@mui/joy/Select";
import Option from "@mui/joy/Option";

import styles from "./FilterTrips.module.css";

const FilterTrips = (props) => {
  const [enteredType, setEnteredType] = useState("");
  const [enteredPreferences, setEnteredPreferences] = useState("");
  const [enteredStartDate, setEnteredStartDate] = useState("");
  const [enteredEndDate, setEnteredEndDate] = useState("");

  const typeDropDownHandler = (event, value) => {
    setEnteredType(value);
  };

  const preferencesDropDownHandler = (event, value) => {
    setEnteredPreferences(value);
  };

  const startDateDropDownHandler = (event) => {
    setEnteredStartDate(event.target.value);
    console.log(new Date(event.target.value).getDate());
    console.log(new Date(event.target.value).getFullYear());
    console.log(new Date(event.target.value).getMonth() + 1);
  };

  const endDateDropDownHandler = (event) => {
    setEnteredEndDate(event.target.value);
  };

  const filterHandler = (event) => {
    event.preventDefault();
    let filteredTrips = props.trips;
    if (enteredType) {
      filteredTrips = filteredTrips.filter(checkType.bind(this, enteredType));
    }
    if (enteredPreferences) {
      filteredTrips = filteredTrips.filter(
        checkPreferences.bind(this, enteredPreferences)
      );
    }
    if (enteredStartDate) {
      filteredTrips = filteredTrips.filter(
        checkStartDate.bind(this, enteredStartDate)
      );
    }
    if (enteredEndDate) {
      filteredTrips = filteredTrips.filter(
        checkEndDate.bind(this, enteredEndDate)
      );
    }
    props.onFilterHandler(filteredTrips);
    props.onResetPage();
  };

  const resetHandler = () => {
    setEnteredStartDate("");
    setEnteredEndDate("");
    setEnteredType("");
    setEnteredPreferences("");
    props.onFilterHandler(props.trips);
    props.onResetPage();
  };

  return (
    <div className={styles["new-trip__control"]}>
      <Typography level="h6" sx={{ mb: 2 }}>
        {props.isFavorite ? "Browse through favorite trips" : "Browse through available trips"}
      </Typography>
      <div className={styles["form-container"]}>
        <FormControl  sx={{ mx: 1 }}>
          <Input
            type="date"
            onChange={startDateDropDownHandler}
            value={enteredStartDate}
            className={styles["start-date"]}
          />
        </FormControl>

        <FormControl  sx={{ mx: 1 }}>
          <Input
            type="date"
            onChange={endDateDropDownHandler}
            value={enteredEndDate}
            className={styles["end-date"]}
          />
        </FormControl>

        <FormControl  sx={{ mx: 1 }}>
          <Select
            placeholder="Choose type"
            onChange={typeDropDownHandler}
            value={enteredType}
            data-testid="type-option"
            sx={{width: 150}}
          >
            <Option value="car">Car trip</Option>
            <Option value="bike">Bike ride</Option>
            <Option value="foot">Hiking trip</Option>
          </Select>
        </FormControl>

        <FormControl sx={{ mx: 1 }}>
          <Select
            placeholder="Choose tags"
            onChange={preferencesDropDownHandler}
            value={enteredPreferences}
            data-testid="preferences-option"
            sx={{width: 160}}
          >
            <Option value="Entertainment">Entertainment</Option>
            <Option value="Sightseeing">Sightseeing</Option>
            <Option value="Exploring">Exploring</Option>
            <Option value="Culture">Culture</Option>
            <Option value="History">History</Option>
            <Option value="Free ride">Free ride</Option>
            <Option value="Training">Training</Option>
            <Option value="Nature">Nature</Option>
          </Select>
        </FormControl>

        <Button
          color="primary"
          type="button"
          onClick={filterHandler}
          sx={{ mx: 1 }}
        >
          Filter
        </Button>
        <Button
          color="primary"
          type="button"
          onClick={resetHandler}
          sx={{ mx: 1 }}
        >
          Reset
        </Button>
      </div>
    </div>
  );
};

function checkType(type, trip) {
  return trip.type === type;
}

function checkPreferences(preference, trip) {
  let existFlag = false;
  for (let item of trip.preferences) {
    if(item.preferenceStr === preference) {
      existFlag = true;
    }
  }
  return existFlag;
}

function checkStartDate(startDate, trip) {
  return new Date(trip.date) >= new Date(startDate);
}

function checkEndDate(endDate, trip) {
  return new Date(trip.date) <= new Date(endDate);
}

export default FilterTrips;
