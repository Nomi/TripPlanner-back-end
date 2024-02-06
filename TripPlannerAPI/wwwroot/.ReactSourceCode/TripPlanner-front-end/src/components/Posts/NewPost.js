import { useContext, useState } from "react";
import { useParams } from "react-router-dom";

import styles from "./NewPost.module.css";
import Textarea from "@mui/joy/Textarea";
import Tooltip from "@mui/joy/Tooltip";
import Button from "@mui/joy/Button";
import SendIcon from "@mui/icons-material/Send";
import LogRegisterContext from "../../contexts/log-register-context";
import fetchUrls from "../../helpers/fetch_urls";

const NewPost = (props) => {
  const [isLoading, setIsLoading] = useState(false);
  const [postContent, setPostContent] = useState("");
  const { token } = useContext(LogRegisterContext);
  const { tripId } = useParams();

  const changeHandler = (event) => {
    setPostContent(event.target.value);
  }

  const submitHandler = async (event) => {
    event.preventDefault();
    setIsLoading(true);
    let postData = {
      content: postContent,
    };

    fetch(`${fetchUrls.posts}/${tripId}`, {
      method: "POST",
      body: JSON.stringify(postData),
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setPostContent("");
        props.onNewPostHandler();
        setIsLoading(false);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className={styles["new-post-control"]}>
      <form onSubmit={submitHandler} className={styles["form-container"]}>
        <Textarea
          minRows={4}
          maxRows={4}
          placeholder="What's on your mind?"
          sx={{ mr: 2, width: "50%" }}
          onChange={changeHandler}
          value={postContent}
        />
        <Tooltip title="Send" variant="soft">
          <Button loading={isLoading} variant="soft" type="submit" onClick={submitHandler} sx={{m: 0, p: 1}}>
            <SendIcon sx={{m: 0, p: 0}} />
          </Button>
        </Tooltip>
      </form>
    </div>
  );
};

export default NewPost;
