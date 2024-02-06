import { Fragment } from "react";
import styles from "./SingleUser.module.css";
import ListItem from "@mui/joy/ListItem";
import Tooltip from "@mui/joy/Tooltip";
import IconButton from "@mui/joy/IconButton";
import EmailIcon from "@mui/icons-material/Email";
import PersonIcon from "@mui/icons-material/Person";
import StarIcon from "@mui/icons-material/Star";
import DeleteIcon from "@mui/icons-material/Delete";

const SingleUser = (props) => {
  const deleteUserHandler = async (username) => {
    props.onDeleteHandler(username);
  };

  return (
    <Fragment>
      <ListItem>
          <div className={styles["post-content"]} data-testid="content">
            {<EmailIcon />} {props.email}&#160;&#160;&#160;{<PersonIcon />}{" "}
            {props.username}
            &#160;&#160;&#160;Organizer Rating:{" "}
            {props.organizerRating.toFixed(2)}&#160;{<StarIcon />}
            &#160;&#160;&#160;User Rating: {props.userRating.toFixed(2)}&#160;
            {<StarIcon />}
          </div>
          <div className={styles["button-content"]}>
            <Tooltip title="Delete user account">
              <IconButton
                variant="plain"
                sx={{ p: 0 }}
                size="sm"
                onClick={(event) => {
                  event.preventDefault();
                  deleteUserHandler(props.username);
                }}
              >
                <DeleteIcon />
              </IconButton>
            </Tooltip>
          </div>
      </ListItem>
    </Fragment>
  );
};

export default SingleUser;
