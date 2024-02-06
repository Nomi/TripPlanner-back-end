import { useCallback, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import fetchUrls from "../../../helpers/fetch_urls";
import styles from "../../NewTrip/NewTrip.module.css";
import PreferencesDescriptionCreate from "../../NewTrip/PreferencesDescriptionCreate";
import DropdownList from "../../NewTrip/DropdownList";
import Map from "../../Map/MapReactLeaflet";
import LogRegisterContext from "../../../contexts/log-register-context";

const NewTripTest = () => {
  const navigate = useNavigate();
  const { token, updateJoinedTrip } = useContext(LogRegisterContext);

  const [enteredType, setEnteredType] = useState("");
  const [enteredPreferences, setEnteredPreferences] = useState([]);
  const [enteredDescription, setEnteredDescription] = useState("");
  const [enteredDate, setEnteredDate] = useState("");
  const [enteredTime, setEnteredTime] = useState("");
  const [enteredWaypoints, setEnteredWaypoints] = useState([]);
  const [calculatedTripData, setCalculatedTripData] = useState(null);

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
  }, []);

  const calculatedTripDataHandler = useCallback((value) => {
    setCalculatedTripData(value);
  }, []);

  const submitFormHandler = async () => {
    let tripData = {
      type: enteredType,
      preferences: enteredPreferences,
      description: enteredDescription,
      date: enteredDate,
      startTime: enteredTime,
      waypoints: enteredWaypoints,
      distance: calculatedTripData.distance,
      totalTime: calculatedTripData.totalTime,
    };
    //console.log(tripData);
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
              onWaypointsHandler={waypointsHandler}
              onCalculatedTripDataHandler={calculatedTripDataHandler}
              typeOfTransport={enteredType}
              userWaypointsInput={[]}
            />
          </div>
        </div>
      </div>
    </section>
  );
};

export default NewTripTest;
