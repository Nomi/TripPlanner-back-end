import { useState, useEffect, useContext } from "react";
import styles from "./Leaderboard.module.css";
import Typography from "@mui/joy/Typography";
import Tabs from "@mui/joy/Tabs";
import TabList from "@mui/joy/TabList";
import Tab from "@mui/joy/Tab";
//import List from "@mui/joy/List";
import Sheet from "@mui/joy/Sheet";
import SingleUserLeaderboard from "./SingleUserLeaderboard";
import LogRegisterContext from "../../contexts/log-register-context";
import SpinnerBox from "../Utils/SpinnerBox";
import fetchUrls from "../../helpers/fetch_urls";

const Leaderboard = () => {
  const [index, setIndex] = useState(0);
  const { token } = useContext(LogRegisterContext);
  const [allFetchedUsers, setAllFetchedUsers] = useState(null);
  const [distanceFetchedUsers, setDistanceFetchedUsers] = useState(null);
  const [organizerFetchedUsers, setOrganizerFetchedUsers] = useState(null);
  const [joinedFetchedUsers, setJoinedFetchedUsers] = useState(null);
  const [ratingOrganizerFetchedUsers, setRatingOrganizerFetchedUsers] =
    useState(null);
  const [ratingUserFetchedUsers, setRatingUserFetchedUsers] = useState(null);
  const [isSendingRequest, setisSendingRequest] = useState(true);

  useEffect(() => {
    fetch(`${fetchUrls.leaderboard}/numTripsJoined`, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let users = [];
        for (let user of data.travellers) {
          users.push(user);
        }
        setJoinedFetchedUsers(users);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(`${fetchUrls.leaderboard}/numTripsCreated`, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let users = [];
        for (let user of data.travellers) {
          users.push(user);
        }
        setOrganizerFetchedUsers(users);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(`${fetchUrls.leaderboard}/userRating`, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let users = [];
        for (let user of data.travellers) {
          users.push(user);
        }
        setRatingUserFetchedUsers(users);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(`${fetchUrls.leaderboard}/organizerRating`, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let users = [];
        for (let user of data.travellers) {
          users.push(user);
        }
        setRatingOrganizerFetchedUsers(users);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(`${fetchUrls.leaderboard}/distance`, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let users = [];
        for (let user of data.travellers) {
          users.push(user);
        }
        setDistanceFetchedUsers(users);
        setAllFetchedUsers(users);
        setisSendingRequest(false);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [token]);

  return (
    <section className={styles["posts-section"]}>
      {isSendingRequest && <SpinnerBox />}
      {allFetchedUsers && (
        <div className={styles["posts"]}>
          <div className={styles["tabs-container"]}>
            <Typography level="h6" sx={{ mb: 2, textAlign: "center" }}>
              Browse through leaderboards
            </Typography>
            <Tabs
              aria-label="Outlined tabs"
              value={index}
              onChange={(event, value) => {
                setIndex(value);
                if (value === 0) {
                  setAllFetchedUsers(distanceFetchedUsers);
                }
                if (value === 1) {
                  setAllFetchedUsers(organizerFetchedUsers);
                }
                if (value === 2) {
                  setAllFetchedUsers(joinedFetchedUsers);
                }
                if (value === 3) {
                  setAllFetchedUsers(ratingOrganizerFetchedUsers);
                }
                if (value === 4) {
                  setAllFetchedUsers(ratingUserFetchedUsers);
                }
              }}
              sx={{ borderRadius: "lg" }}
            >
              <TabList variant="outlined">
                <Tab
                  variant={index === 0 ? "soft" : "plain"}
                  color={index === 0 ? "primary" : "neutral"}
                >
                  Longest traveller
                </Tab>
                <Tab
                  variant={index === 1 ? "soft" : "plain"}
                  color={index === 1 ? "primary" : "neutral"}
                >
                  Most frequent organizer
                </Tab>
                <Tab
                  variant={index === 2 ? "soft" : "plain"}
                  color={index === 2 ? "primary" : "neutral"}
                >
                  Most frequent participant
                </Tab>
                <Tab
                  variant={index === 3 ? "soft" : "plain"}
                  color={index === 3 ? "primary" : "neutral"}
                >
                  Best organizer
                </Tab>
                <Tab
                  variant={index === 4 ? "soft" : "plain"}
                  color={index === 4 ? "primary" : "neutral"}
                >
                  Best participant
                </Tab>
              </TabList>
            </Tabs>
          </div>
          <Sheet
            sx={{
              height: "85%",
              width: "100%",
              borderRadius: "md",
            }}
          >
            <table className={styles["table-style"]}>
                {index === 0 && (
                  <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Username</th>
                    <th scope="col">Distance covered</th>
                    <th scope="col">Trips organized</th>
                    <th scope="col">Trips joined</th>
                    <th scope="col">Organizer rating</th>
                    <th scope="col">Participant rating</th>
                  </tr>
                )}
                {index === 1 && (
                  <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Username</th>
                    <th scope="col">Trips organized</th>
                    <th scope="col">Trips joined</th>
                    <th scope="col">Organizer rating</th>
                    <th scope="col">Participant rating</th>
                    <th scope="col">Distance covered</th>
                  </tr>
                )}
                {index === 2 && (
                  <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Username</th>
                    <th scope="col">Trips joined</th>
                    <th scope="col">Trips organized</th>
                    <th scope="col">Organizer rating</th>
                    <th scope="col">Participant rating</th>
                    <th scope="col">Distance covered</th>
                  </tr>
                )}
                {index === 3 && (
                  <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Username</th>
                    <th scope="col">Organizer rating</th>
                    <th scope="col">Participant rating</th>
                    <th scope="col">Trips organized</th>
                    <th scope="col">Trips joined</th>
                    <th scope="col">Distance covered</th>
                  </tr>
                )}
                {index === 4 && (
                  <tr>
                    <th scope="col">Rank</th>
                    <th scope="col">Username</th>
                    <th scope="col">Participant rating</th>
                    <th scope="col">Organizer rating</th>
                    <th scope="col">Trips organized</th>
                    <th scope="col">Trips joined</th>
                    <th scope="col">Distance covered</th>
                  </tr>
                )}
                {allFetchedUsers.map((user, indexNumber) => (
                  <SingleUserLeaderboard
                    key={user.username}
                    index={indexNumber}
                    username={user.username}
                    distance={user.distance}
                    numTripsCreated={user.numTripsCreated}
                    numTripsJoined={user.numTripsJoined}
                    organizerRating={user.organizerRating}
                    userRating={user.userRating}
                    type={index}
                  ></SingleUserLeaderboard>
                ))}
            </table>
          </Sheet>
        </div>
      )}
    </section>
  );
};

export default Leaderboard;
