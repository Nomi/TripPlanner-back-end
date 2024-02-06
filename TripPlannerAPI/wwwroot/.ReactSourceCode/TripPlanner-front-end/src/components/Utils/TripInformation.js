import Typography from "@mui/joy/Typography";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import ScheduleIcon from "@mui/icons-material/Schedule";
import PersonIcon from "@mui/icons-material/Person";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import DirectionsWalkIcon from "@mui/icons-material/DirectionsWalk";
import DirectionsBikeIcon from "@mui/icons-material/DirectionsBike";
import StarIcon from "@mui/icons-material/Star";

import styles from "./TripInformation.module.css";
import { options } from "../../helpers/helpers";

const TripInformation = (props) => {
  const date = new Date(props.tripData.date);
  let year = date.getFullYear();
  let month = date.getMonth() + 1;
  let day = date.getDate();

  if (day < 10) {
    day = "0" + day;
  }
  if (month < 10) {
    month = "0" + month;
  }

  return (
    <div className={styles["new-trip-control"]}>
      <Typography level="h6" sx={{ mb: 2 }}>
        Find out more about the trip
      </Typography>
      <div className={styles["form-container"]}>
        <Typography level="body1" startDecorator={<CalendarMonthIcon />} data-testid="trip-date">
          Trip date: {day}.{month}.{year}
        </Typography>
        <Typography
          level="body1"
          sx={{ ml: 8, mr: 4 }}
          startDecorator={<ScheduleIcon />}
        >
          Trip time: {props.tripData.startTime}
        </Typography>
        {props.tripData.type === "car" && (
          <Typography
            level="body1"
            sx={{ ml: 4, mr: 8 }}
            startDecorator={<DirectionsCarIcon />}
            data-testid="trip-type"
          >
            Trip type: {options[props.tripData.type]}
          </Typography>
        )}
        {props.tripData.type === "bike" && (
          <Typography
            level="body1"
            sx={{ ml: 4, mr: 8 }}
            startDecorator={<DirectionsBikeIcon />}
            data-testid="trip-type"
          >
            Trip type: {options[props.tripData.type]}
          </Typography>
        )}
        {props.tripData.type === "foot" && (
          <Typography
            level="body1"
            sx={{ ml: 4, mr: 8 }}
            startDecorator={<DirectionsWalkIcon />}
            data-testid="trip-type"
          >
            Trip type: {options[props.tripData.type]}
          </Typography>
        )}
        <Typography
          level="body1"
          startDecorator={<PersonIcon />}
          endDecorator={<StarIcon sx={{ mx: -0.5 }} />}
        >
          Created by: {props.tripData.creator.username} &#160;
          {props.tripData.creator.userRating.toFixed(2)}
        </Typography>
      </div>
    </div>
  );
};

export default TripInformation;
