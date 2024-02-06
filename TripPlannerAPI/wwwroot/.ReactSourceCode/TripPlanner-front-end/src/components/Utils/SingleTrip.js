import { useContext } from "react";
import { Link as LinkRouter, useNavigate } from "react-router-dom";
import Link from "@mui/joy/Link";

import Sheet from "@mui/joy/Sheet";
import ListItem from "@mui/joy/ListItem";
import Typography from "@mui/joy/Typography";
import { options } from "../../helpers/helpers";
import LogRegisterContext from "../../contexts/log-register-context";
import fetchUrls from "../../helpers/fetch_urls";
import Tooltip from "@mui/joy/Tooltip";
import IconButton from "@mui/joy/IconButton";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import ArrowRightAltIcon from "@mui/icons-material/ArrowRightAlt";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import DirectionsWalkIcon from "@mui/icons-material/DirectionsWalk";
import DirectionsBikeIcon from "@mui/icons-material/DirectionsBike";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import FavoriteIcon from "@mui/icons-material/Favorite";
import ThumbUpAltIcon from "@mui/icons-material/ThumbUpAlt";

const SingleTrip = (props) => {
  const { token } = useContext(LogRegisterContext);
  const navigate = useNavigate();

  const date = new Date(props.tripData.date);
  let month = date.getMonth() + 1;
  const day = date.toLocaleString("en-US", { day: "2-digit" });
  const year = date.getFullYear();
  if (month < 10) {
    month = "0" + month;
  }

  const addFavoritesHandler = async () => {
    fetch(`${fetchUrls["add-favorite-trips"]}/${props.tripData.tripId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        navigate("/favorite-trips", { replace: true });
      })
      .catch((error) => {
        console.log(error);
      });
  };

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
          sx={{
            my: 0.5,
            textAlign: "center",
            display: "flex",
            alignItems: "center",
          }}
          data-testid="date-type"
        >
          {props.tripData.isRecommended && <ThumbUpAltIcon sx={{mr: 0.25}} />}
          {props.tripData.isRecommended && (
            <Typography sx={{fontStyle: "italic", mr: 2}}>Recommended</Typography>
          )}
          <CalendarMonthIcon sx={{ mr: 0.5 }} />
          <Typography sx={{mr: 2}}>{day}.{month}.{year}</Typography>
          {props.tripData.type === "car" && (
            <DirectionsCarIcon sx={{ mr: 0.5 }} />
          )}
          {props.tripData.type === "bike" && (
            <DirectionsBikeIcon sx={{ mr: 0.75 }} />
          )}
          {props.tripData.type === "foot" && (
            <DirectionsWalkIcon sx={{ mr: 0.5 }} />
          )}
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
        <Sheet
          color="primary"
          variant="soft"
          sx={{ display: "flex", alignItems: "center" }}
        >
          <Link
            component={LinkRouter}
            to={`${props.tripData.tripId}`}
            sx={{ mt: 0.5, textAlign: "center" }}
          >
            Show details
          </Link>
          {!props.tripData.isFavoriteForCurrentUser && (
            <Tooltip title="Add to favorites">
              <IconButton
                variant="plain"
                sx={{ p: 0 }}
                size="sm"
                onClick={addFavoritesHandler}
              >
                <FavoriteBorderIcon />
              </IconButton>
            </Tooltip>
          )}
          {props.tripData.isFavoriteForCurrentUser && (
            <IconButton variant="plain" sx={{ p: 0 }} size="sm">
              <FavoriteIcon />
            </IconButton>
          )}
        </Sheet>
      </Sheet>
    </ListItem>
  );
};

export default SingleTrip;
