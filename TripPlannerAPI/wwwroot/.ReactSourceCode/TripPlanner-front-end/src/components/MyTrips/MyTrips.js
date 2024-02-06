import { useState, useEffect, useReducer, useContext } from "react";
import Tabs from "@mui/joy/Tabs";
import TabList from "@mui/joy/TabList";
import Tab from "@mui/joy/Tab";
import Typography from "@mui/joy/Typography";
import List from "@mui/joy/List";

import styles from "./MyTrips.module.css";
import SingleTrip from "../Utils/SingleTrip";
import PaginationList from "../Utils/PaginationList";
import SpinnerBox from "../Utils/SpinnerBox";
import LogRegisterContext from "../../contexts/log-register-context";
import fetchUrls from "../../helpers/fetch_urls";

const initialState = {
  tripsPerPage: 4,
  currentPage: 1,
  firstIndex: 0,
  lastIndex: 4,
};

const reducer = (state, action) => {
  switch (action.type) {
    case "increment":
      return {
        ...state,
        currentPage: Number(state.currentPage) + 1,
        firstIndex: Number(state.currentPage) * Number(state.tripsPerPage),
        lastIndex: Number(state.currentPage) * Number(state.tripsPerPage) + 4,
      };
    case "decrement":
      return {
        ...state,
        currentPage: Number(state.currentPage) - 1,
        firstIndex:
          (Number(state.currentPage) - 2) * Number(state.tripsPerPage),
        lastIndex:
          (Number(state.currentPage) - 2) * Number(state.tripsPerPage) + 4,
      };
    case "change":
      return {
        ...state,
        currentPage: Number(action.page),
        firstIndex: (Number(action.page) - 1) * Number(state.tripsPerPage),
        lastIndex: Number(action.page) * Number(state.tripsPerPage),
      };
    case "reset":
      return {
        ...state,
        currentPage: 1,
        firstIndex: 0,
        lastIndex: 4,
      };
    default:
      throw new Error();
  }
};

const MyTrips = () => {
  const { token, joinedTrip } = useContext(LogRegisterContext);
  const [index, setIndex] = useState(0);
  const [allTrips, setAllTrips] = useState(null);
  const [createdFutureTrips, setCreatedFutureTrips] = useState(null);
  const [joinedFutureTrips, setJoinedFutureTrips] = useState(null);
  const [createdPastTrips, setCreatedPastTrips] = useState(null);
  const [joinedPastTrips, setJoinedPastTrips] = useState(null);
  const [paginationState, dispatch] = useReducer(reducer, initialState);

  useEffect(() => {
    fetch(fetchUrls["get-my-trips"] + "/created_past", {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        let trips = [];
        for (const trip of data.trips) {
          trips.unshift(trip);
        }
        setCreatedPastTrips(trips);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(fetchUrls["get-my-trips"] + "/joined_past", {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        let trips = [];
        for (const trip of data.trips) {
          trips.unshift(trip);
        }
        setJoinedPastTrips(trips);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(fetchUrls["get-my-trips"] + "/joined_future", {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        let trips = [];
        for (const trip of data.trips) {
          trips.unshift(trip);
        }
        console.log(trips)
        setJoinedFutureTrips(trips);
        if (joinedTrip === true) {
          setAllTrips(trips);
          setIndex(1);
        }
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(fetchUrls["get-my-trips"] + "/created_future", {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        let trips = [];
        for (const trip of data.trips) {
          trips.unshift(trip);
        }
        setCreatedFutureTrips(trips);
        if (joinedTrip === false) {
          setAllTrips(trips);
          setIndex(0);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }, [token, joinedTrip]);

  const nextClickHandler = () => {
    dispatch({ type: "increment" });
  };
  const previousClickHandler = () => {
    dispatch({ type: "decrement" });
  };
  const pageNumberClickHandler = (pageNumber) => {
    dispatch({ type: "change", page: pageNumber });
  };
  const resetPage = () => {
    dispatch({ type: "reset" });
  };

  return (
    <section>
      {!allTrips && <SpinnerBox />}
      {allTrips && (
        <div className={styles["tabs-container"]}>
          <Typography level="h6" sx={{ mb: 2, textAlign: "center" }}>
            Browse through my trips
          </Typography>
          <Tabs
            aria-label="Outlined tabs"
            value={index}
            onChange={(event, value) => {
              setIndex(value);
              if (value === 0) {
                setAllTrips(createdFutureTrips);
              }
              if (value === 1) {
                setAllTrips(joinedFutureTrips);
              }
              if (value === 2) {
                setAllTrips(createdPastTrips);
              }
              if (value === 3) {
                setAllTrips(joinedPastTrips);
              }
              resetPage();
            }}
            sx={{ borderRadius: "lg" }}
          >
            <TabList variant="outlined">
              <Tab
                variant={index === 0 ? "soft" : "plain"}
                color={index === 0 ? "primary" : "neutral"}
              >
                Created future trips
              </Tab>
              <Tab
                variant={index === 1 ? "soft" : "plain"}
                color={index === 1 ? "primary" : "neutral"}
              >
                Joined future trips
              </Tab>
              <Tab
                variant={index === 2 ? "soft" : "plain"}
                color={index === 2 ? "primary" : "neutral"}
              >
                Created past trips
              </Tab>
              <Tab
                variant={index === 3 ? "soft" : "plain"}
                color={index === 3 ? "primary" : "neutral"}
              >
                Joined past trips
              </Tab>
            </TabList>
          </Tabs>
        </div>
      )}
      {allTrips &&
        (allTrips.length === 0 ? (
          <p className={styles["no-trips-found"]}>No trips found</p>
        ) : (
          <List sx={{ width: "40%", p: 0, m: "0 auto", mb: 2 }}>
            {allTrips
              .slice(paginationState.firstIndex, paginationState.lastIndex)
              .map((trip) => (
                <SingleTrip key={trip.tripId} tripData={trip}></SingleTrip>
              ))}
          </List>
        ))}
      {allTrips && allTrips.length > 4 && (
        <PaginationList
          sumOfTrips={allTrips.length}
          paginationState={paginationState}
          onPreviousHandler={previousClickHandler}
          onNextHandler={nextClickHandler}
          onPageNumberHandler={pageNumberClickHandler}
        />
      )}
    </section>
  );
};

export default MyTrips;
