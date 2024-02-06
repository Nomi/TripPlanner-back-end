import { Fragment } from "react";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import StarIcon from "@mui/icons-material/Star";

const SingleUserLeaderboard = (props) => {
  return (
    <Fragment>
      {props.type === 0 && (
        <tr>
          {props.index === 0 && (
            <td>
              {<EmojiEventsIcon sx={{ mr: 0.25 }} />}
              {props.index + 1}
            </td>
          )}
          {props.index !== 0 && <td>{props.index + 1}</td>}
          <td>{props.username}</td>
          <td>{props.distance}km</td>
          <td>{props.numTripsCreated}</td>
          <td>{props.numTripsJoined}</td>
          <td>
            {props.organizerRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>
            {props.userRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
        </tr>
      )}
      {props.type === 1 && (
        <tr>
          {props.index === 0 && (
            <td>
              {<EmojiEventsIcon sx={{ mr: 0.25 }} />}
              {props.index + 1}
            </td>
          )}
          {props.index !== 0 && <td>{props.index + 1}</td>}
          <td>{props.username}</td>
          <td>{props.numTripsCreated}</td>
          <td>{props.numTripsJoined}</td>
          <td>
            {props.organizerRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>
            {props.userRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>{props.distance}km</td>
        </tr>
      )}
      {props.type === 2 && (
        <tr>
          {props.index === 0 && (
            <td>
              {<EmojiEventsIcon sx={{ mr: 0.25 }} />}
              {props.index + 1}
            </td>
          )}
          {props.index !== 0 && <td>{props.index + 1}</td>}
          <td>{props.username}</td>
          <td>{props.numTripsJoined}</td>
          <td>{props.numTripsCreated}</td>
          <td>
            {props.organizerRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>
            {props.userRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>{props.distance}km</td>
        </tr>
      )}
      {props.type === 3 && (
        <tr>
          {props.index === 0 && (
            <td>
              {<EmojiEventsIcon sx={{ mr: 0.25 }} />}
              {props.index + 1}
            </td>
          )}
          {props.index !== 0 && <td>{props.index + 1}</td>}
          <td>{props.username}</td>
          <td>
            {props.organizerRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>
            {props.userRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>{props.numTripsCreated}</td>
          <td>{props.numTripsJoined}</td>
          <td>{props.distance}km</td>
        </tr>
      )}
      {props.type === 4 && (
        <tr>
          {props.index === 0 && (
            <td>
              {<EmojiEventsIcon sx={{ mr: 0.25 }} />}
              {props.index + 1}
            </td>
          )}
          {props.index !== 0 && <td>{props.index + 1}</td>}
          <td>{props.username}</td>
          <td>
            {props.userRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>
            {props.organizerRating.toFixed(2)}
            {<StarIcon sx={{ ml: 0.25 }} />}
          </td>
          <td>{props.numTripsCreated}</td>
          <td>{props.numTripsJoined}</td>
          <td>{props.distance}km</td>
        </tr>
      )}
    </Fragment>
  );
};

export default SingleUserLeaderboard;
