import { Fragment } from "react";
import styles from "./SinglePost.module.css";
import Sheet from "@mui/joy/Sheet";
import ListItem from "@mui/joy/ListItem";
import PersonIcon from "@mui/icons-material/Person";

const SinglePost = (props) => {
  return (
    <Fragment>
      <div className={styles["post-date"]} data-testid="date">
        {props.day} {props.month} {props.year}
      </div>
      <ListItem>
        <Sheet
          color="primary"
          variant="soft"
          sx={{
            p: 1,
            width: "100%",
            borderRadius: "sm",
          }}
        >
          <div className={styles["post-content"]} data-testid="content">
            {props.content}
          </div>
          <div className={styles["post-author"]} data-testid="author">
            {<PersonIcon />}{props.author}
          </div>
        </Sheet>
      </ListItem>
    </Fragment>
  );
};

export default SinglePost;
