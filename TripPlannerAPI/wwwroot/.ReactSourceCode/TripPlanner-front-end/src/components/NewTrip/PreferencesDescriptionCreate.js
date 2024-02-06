import Textarea from "@mui/joy/Textarea";
import Checkbox from "@mui/joy/Checkbox";
import List from "@mui/joy/List";
import ListItem from "@mui/joy/ListItem";
import Typography from "@mui/joy/Typography";
import Sheet from "@mui/joy/Sheet";
import Done from "@mui/icons-material/Done";
import Weather from "./Weather";

import styles from "./PreferencesDescriptionCreate.module.css";

const PreferencesDescriptionCreate = (props) => {
  let isDestinationAvailable = props.wayPoints.length > 1;

  return (
    <div className={styles["content-container"]}>
      <Textarea
        minRows={6}
        maxRows={7}
        placeholder="Tell us more about the planned trip so that other users will know what to expect ... "
        sx={{ mb: 2 }}
        onBlur={(event) => props.onDescriptionHandler(event.target.value)}
      />
      <Sheet
        variant="outlined"
        sx={{
          p: 2,
          borderRadius: "sm",
          mb: 2,
          overflow: "auto",
          height: "40%",
        }}
      >
        <Typography sx={{ mb: 1.5 }} level="body1">
          Choose tags that most accurately describe your trip
        </Typography>

        <List
          row
          wrap
          sx={{
            "--List-gap": "16px",
            "--List-item-radius": "20px",
            "--List-item-minHeight": "32px",
          }}
        >
          {[
            "Entertainment",
            "Sightseeing",
            "Exploring",
            "Culture",
            "History",
            "Free ride",
            "Training",
            "Nature",
          ].map((item) => (
            <ListItem key={item}>
              {props.enteredPreferences.includes(item) && (
                <Done
                  fontSize="md"
                  color="primary"
                  sx={{ mr: 0.5, zIndex: 2 }}
                />
              )}
              <Checkbox
                size="sm"
                disableIcon
                overlay
                label={item}
                checked={props.enteredPreferences.includes(item)}
                variant={
                  props.enteredPreferences.includes(item) ? "soft" : "outlined"
                }
                onChange={(event) => {
                  if (event.target.checked) {
                    props.onPreferencesHandler([
                      ...props.enteredPreferences,
                      item,
                    ]);
                  } else {
                    props.onPreferencesHandler(
                      props.enteredPreferences.filter((text) => text !== item)
                    );
                  }
                }}
              />
            </ListItem>
          ))}
        </List>
      </Sheet>
      {props.calculatedTripData && (
        <Sheet
          variant="outlined"
          sx={{
            p: 2,
            borderRadius: "sm",
            mb: 2,
          }}
        >
          <Typography level="body1">
            Trip distance: {props.calculatedTripData.distance} km
          </Typography>
          <Typography level="body1">
            Estimated time: {props.calculatedTripData.timeHours} h{" "}
            {props.calculatedTripData.timeMinutes} min
          </Typography>
        </Sheet>
      )}
      {props.calculatedTripData && (
        <Weather
          enteredWayPoints={props.wayPoints[props.wayPoints.length - 1]}
          isDestinantion={isDestinationAvailable}
        />
      )}
    </div>
  );
};

export default PreferencesDescriptionCreate;
