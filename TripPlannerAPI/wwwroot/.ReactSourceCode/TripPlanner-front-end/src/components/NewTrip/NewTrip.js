import { React, useCallback, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import fetchUrls from "../../helpers/fetch_urls";
import styles from "./NewTrip.module.css";
import PreferencesDescriptionCreate from "./PreferencesDescriptionCreate";
import DropdownList from "./DropdownList";
import Map from "../Map/Map";
import LogRegisterContext from "../../contexts/log-register-context";

const NewTrip = () => {
  const navigate = useNavigate();
  const { token, updateJoinedTrip } = useContext(LogRegisterContext);

  const [enteredType, setEnteredType] = useState("car");
  const [enteredPreferences, setEnteredPreferences] = useState([]);
  const [enteredDescription, setEnteredDescription] = useState("");
  const [enteredDate, setEnteredDate] = useState("");
  const [enteredTime, setEnteredTime] = useState("");
  const [enteredWaypoints, setEnteredWaypoints] = useState([]);
  const [enteredPins, setEnteredPins] = useState([]);
  const [calculatedTripData, setCalculatedTripData] = useState(null);
  const [isSendingRequest, setIsSendingRequest] = useState(false);

  let enableCreateButtonFlag = !!(
    enteredType &&
    enteredPreferences.length &&
    enteredDescription &&
    enteredDate &&
    enteredTime &&
    enteredWaypoints.length
  );

  const typeHandler = (value) => {
    setEnteredType(value);
  };

  const preferencesHandler = (value) => {
    setEnteredPreferences(value);
  };

  const descriptionHandler = (value) => {
    setEnteredDescription(value);
  };

  const dateHandler = (value) => {
    let tripDate = new Date(value).toJSON();
    setEnteredDate(tripDate);
  };

  const timeHandler = (value) => {
    setEnteredTime(value);
  };

  const waypointsHandler = useCallback((value) => {
    setEnteredWaypoints(value);
    //console.log("test");
  }, []);

  const calculatedTripDataHandler = useCallback((value) => {
    setCalculatedTripData(value);
    //console.log("test2");
  }, []);

  const pinsHandler = useCallback((value) => {
    setEnteredPins(value);
    console.log(value);
  }, []);

  const submitFormHandler = async () => {
    setIsSendingRequest(true);
    let tripData = {
      type: enteredType,
      preferences: enteredPreferences,
      description: enteredDescription,
      date: enteredDate,
      startTime: enteredTime,
      waypoints: enteredWaypoints,
      pins: enteredPins,
      distance: calculatedTripData.distance,
      totalTime: calculatedTripData.totalTime,
    };
    console.log(JSON.stringify(tripData));
    const response = await fetch(fetchUrls["create-trip"], {
      method: "POST",
      body: JSON.stringify(tripData),
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    });
    const data = await response.json();
    if (!response.ok) {
      alert(data.message || "Could not create trip.");
    } else {
      updateJoinedTrip(false);
      setIsSendingRequest(false);
      navigate("/my-trips", { replace: true });
    }
  };

  return (
    <section className={styles["new-trip-section"]}>
      <div className={styles["new-trip"]}>
        <DropdownList
          onTypeHandler={typeHandler}
          onDateHandler={dateHandler}
          onTimeHandler={timeHandler}
          onSubmitFormHandler={submitFormHandler}
          enableCreateButtonFlag={enableCreateButtonFlag}
          enteredType={enteredType}
          isSendingRequest={isSendingRequest}
        />
        <div className={styles["map-details-container"]}>
          <PreferencesDescriptionCreate
            onDescriptionHandler={descriptionHandler}
            onPreferencesHandler={preferencesHandler}
            enteredPreferences={enteredPreferences}
            calculatedTripData={calculatedTripData}
            wayPoints={enteredWaypoints}
          />
          <div className={styles["map-only-container"]}>
            <Map
              onPinsHandler={pinsHandler}
              onWaypointsHandler={waypointsHandler}
              onCalculatedTripDataHandler={calculatedTripDataHandler}
              typeOfTransport={enteredType}
              userWaypointsInput={enteredWaypoints}
              userPinsInput={enteredPins}
              staticMap={false}
              isTripCreated={true}
            />
          </div>
        </div>
      </div>
    </section>
  );
};

export default NewTrip;