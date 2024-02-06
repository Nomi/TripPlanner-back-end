import { useState, useEffect, useContext } from "react";
import Tabs from "@mui/joy/Tabs";
import TabList from "@mui/joy/TabList";
import Tab from "@mui/joy/Tab";
import TabPanel from "@mui/joy/TabPanel";

import SpinnerBox from "../Utils/SpinnerBox";
import LogRegisterContext from "../../contexts/log-register-context";
import fetchUrls from "../../helpers/fetch_urls";
import { tripPreferences } from "../../helpers/helpers";
import Preferences from "./Preferences";
import Typography from "@mui/joy/Typography";
import Sheet from "@mui/joy/Sheet";

const UserProfile = () => {
  const { token } = useContext(LogRegisterContext);
  const [index, setIndex] = useState(0);
  const [currentUser, setCurrentUser] = useState(null);
  const [allPreferences, setAllPreferences] = useState(null);
  const [preferencesCar, setPreferencesCar] = useState(null);
  const [preferencesBike, setPreferencesBike] = useState(null);
  const [preferencesFoot, setPreferencesFoot] = useState(null);
  const [updatePreferences, setUpdatePreferences] = useState(0);
  const [isSendingRequest, setIsSendingRequest] = useState(false);

  useEffect(() => {
    fetch(fetchUrls["current-user"], {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => response.json())
      .then((data) => {
        setCurrentUser(data);
        console.log(data);
      })
      .catch((error) => {
        console.log(error);
      });

    fetch(fetchUrls.preferences, {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => {
        if (response.status === 204) {
          setPreferencesCar(tripPreferences.tripTypePreferences);
          setPreferencesBike(tripPreferences.tripTypePreferences);
          setPreferencesFoot(tripPreferences.tripTypePreferences);
          setAllPreferences(tripPreferences.tripTypePreferences);
        } else {
          return response.json();
        }
      })
      .then((data) => {
        if (data) {
          data[0]
            ? setPreferencesCar(data[0].tripTypePreferences)
            : setPreferencesCar(tripPreferences.tripTypePreferences);
          data[1]
            ? setPreferencesBike(data[1].tripTypePreferences)
            : setPreferencesBike(tripPreferences.tripTypePreferences);
          data[2]
            ? setPreferencesFoot(data[2].tripTypePreferences)
            : setPreferencesFoot(tripPreferences.tripTypePreferences);
          data[0]
            ? setAllPreferences(data[0].tripTypePreferences)
            : setAllPreferences(tripPreferences.tripTypePreferences);
        }
        setIsSendingRequest(false);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [token, updatePreferences]);

  const savePreferences = (tripType, preferences) => {
    setIsSendingRequest(true);
    let tripTypeId;
    tripType === "car" && (tripTypeId = 1);
    tripType === "bike" && (tripTypeId = 2);
    tripType === "foot" && (tripTypeId = 3);

    let tripPreferencesData = {
      tripTypeId: tripTypeId,
      tripTypeName: tripType,
      tripTypePreferences: [
        {
          preferenceId: 1,
          preferenceName: "Entertainment",
          points: preferences.entertainment,
        },
        {
          preferenceId: 2,
          preferenceName: "Sightseeing",
          points: preferences.sightseeing,
        },
        {
          preferenceId: 3,
          preferenceName: "Exploring",
          points: preferences.exploring,
        },
        {
          preferenceId: 4,
          preferenceName: "Culture",
          points: preferences.culture,
        },
        {
          preferenceId: 5,
          preferenceName: "History",
          points: preferences.history,
        },
        {
          preferenceId: 6,
          preferenceName: "Free ride",
          points: preferences.freeride,
        },
        {
          preferenceId: 7,
          preferenceName: "Training",
          points: preferences.training,
        },
        {
          preferenceId: 8,
          preferenceName: "Nature",
          points: preferences.nature,
        },
      ],
    };
    console.log(tripPreferencesData);

    fetch(fetchUrls.preferences, {
      method: "POST",
      body: JSON.stringify(tripPreferencesData),
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then((response) => {
        if (response.ok) {
          setUpdatePreferences((previousState) => previousState + 1);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <section style={{ height: "100%" }}>
      {(!currentUser || !allPreferences) && <SpinnerBox />}
      {currentUser && allPreferences && (
        <>
          <Typography level="h6" sx={{ mb: 1.5, textAlign: "center" }}>
            Personal information
          </Typography>
          <Sheet sx={{ mb: 2, textAlign: "center" }}>
            <Typography level="body1" sx={{ fontStyle: "italic" }}>
              Email: {currentUser.email}
            </Typography>
            <Typography level="body1" sx={{ fontStyle: "italic" }}>
              Username: {currentUser.userName}
            </Typography>
          </Sheet>
          <Typography level="h6" sx={{ mb: 2, textAlign: "center" }}>
            Trip preferences
          </Typography>
          <Sheet sx={{ width: "50%", m: "0 auto", mb: 4 }}>
            <Tabs
              aria-label="Outlined tabs"
              value={index}
              sx={{ borderRadius: "lg" }}
              onChange={(event, value) => {
                setIndex(value);
                if (value === 0) {
                  setPreferencesCar(preferencesCar);
                }
                if (value === 1) {
                  setPreferencesBike(preferencesBike);
                }
                if (value === 2) {
                  setPreferencesFoot(preferencesFoot);
                }
              }}
            >
              <TabList variant="outlined">
                <Tab
                  variant={index === 0 ? "soft" : "plain"}
                  color={index === 0 ? "primary" : "neutral"}
                >
                  Car trip
                </Tab>
                <Tab
                  variant={index === 1 ? "soft" : "plain"}
                  color={index === 1 ? "primary" : "neutral"}
                >
                  Bike ride
                </Tab>
                <Tab
                  variant={index === 2 ? "soft" : "plain"}
                  color={index === 2 ? "primary" : "neutral"}
                >
                  Hiking trip
                </Tab>
              </TabList>
              <TabPanel
                value={0}
                sx={{
                  mt: 2,
                  width: 400,
                  alignSelf: "center",
                  textAlign: "center",
                }}
              >
                <Preferences
                  onSavePreferences={savePreferences}
                  tripType="car"
                  isSendingRequest={isSendingRequest}
                  entertainment={preferencesCar[0].points}
                  sightseeing={preferencesCar[1].points}
                  exploring={preferencesCar[2].points}
                  culture={preferencesCar[3].points}
                  history={preferencesCar[4].points}
                  freeride={preferencesCar[5].points}
                  training={preferencesCar[6].points}
                  nature={preferencesCar[7].points}
                />
              </TabPanel>
              <TabPanel
                value={1}
                sx={{
                  mt: 2,
                  width: 400,
                  alignSelf: "center",
                  textAlign: "center",
                }}
              >
                <Preferences
                  onSavePreferences={savePreferences}
                  tripType="bike"
                  isSendingRequest={isSendingRequest}
                  entertainment={preferencesBike[0].points}
                  sightseeing={preferencesBike[1].points}
                  exploring={preferencesBike[2].points}
                  culture={preferencesBike[3].points}
                  history={preferencesBike[4].points}
                  freeride={preferencesBike[5].points}
                  training={preferencesBike[6].points}
                  nature={preferencesBike[7].points}
                />
              </TabPanel>
              <TabPanel
                value={2}
                sx={{
                  mt: 2,
                  width: 400,
                  alignSelf: "center",
                  textAlign: "center",
                }}
              >
                <Preferences
                  onSavePreferences={savePreferences}
                  tripType="foot"
                  isSendingRequest={isSendingRequest}
                  entertainment={preferencesFoot[0].points}
                  sightseeing={preferencesFoot[1].points}
                  exploring={preferencesFoot[2].points}
                  culture={preferencesFoot[3].points}
                  history={preferencesFoot[4].points}
                  freeride={preferencesFoot[5].points}
                  training={preferencesFoot[6].points}
                  nature={preferencesFoot[7].points}
                />
              </TabPanel>
            </Tabs>
          </Sheet>
        </>
      )}
    </section>
  );
};

export default UserProfile;
