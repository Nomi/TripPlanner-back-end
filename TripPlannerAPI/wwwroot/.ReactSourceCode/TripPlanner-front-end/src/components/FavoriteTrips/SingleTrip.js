import { Link as LinkRouter } from "react-router-dom";
import Link from "@mui/joy/Link";

import Sheet from "@mui/joy/Sheet";
import ListItem from "@mui/joy/ListItem";
import Typography from "@mui/joy/Typography";
import { options } from "../../helpers/helpers";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import ArrowRightAltIcon from "@mui/icons-material/ArrowRightAlt";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import DirectionsWalkIcon from "@mui/icons-material/DirectionsWalk";
import DirectionsBikeIcon from "@mui/icons-material/DirectionsBike";

const SingleTrip = (props) => {
  const date = new Date(props.tripData.date);
  let month = date.getMonth() + 1;
  const day = date.toLocaleString("en-US", { day: "2-digit" });
  const year = date.getFullYear();
  if (month < 10) {
    month = "0" + month;
  }

  console.log(props.tripData);

  return (
    <ListItem sx={{ width: "100%" }}>
      <Sheet
        color="primary"
        variant="soft"
        sx={{
          display: "flex",
          p: 1.5,
          alignItems: "center",
          justifyContent: "center",
          width: "100%",
          borderRadius: "sm",
          flexDirection: "column",
        }}
      >
        <Typography
          level="body1"
          startDecorator={<CalendarMonthIcon />}
          sx={{ my: 0.5, textAlign: "center" }}
          data-testid="date-type"
        >
          {day}.{month}.{year} &#160; &#160;
          {props.tripData.type === "car" && <DirectionsCarIcon sx={{mr: 0.5}} />}
          {props.tripData.type === "bike" && <DirectionsBikeIcon sx={{mr: 0.75}} />}
          {props.tripData.type === "foot" && <DirectionsWalkIcon sx={{mr: 0.5}} />}
          {options[props.tripData.type]}
        </Typography>
        <Typography
          level="body1"
          sx={{ my: 0.5, textAlign: "center" }}
          data-testid="waypoints"
        >
          {props.tripData.waypoints[0].name.split(",")[0]}
          <ArrowRightAltIcon sx={{ mx: 0.5 }} />
          {
            props.tripData.waypoints[
              props.tripData.waypoints.length - 1
            ].name.split(",")[0]
          }
        </Typography>
        <Link
          component={LinkRouter}
          to={`${props.tripData.tripId}`}
          sx={{ mt: 0.5, textAlign: "center" }}
        >
          Show details
        </Link>
      </Sheet>
    </ListItem>
  );
};

export default SingleTrip;
