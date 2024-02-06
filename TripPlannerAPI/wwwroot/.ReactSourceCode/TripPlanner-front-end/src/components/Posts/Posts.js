import { useState } from "react";

import styles from "./Posts.module.css";
import NewPost from "./NewPost";
import AllPosts from "./AllPosts";

const Posts = () => {
  const [isNewPost, setIsNewPost] = useState(0);
  const newPostHandler = () => {
    setIsNewPost((previousState) => previousState + 1);
  };

  return (
    <section className={styles["posts-section"]}>
      <div className={styles["posts"]}>
        <AllPosts isNewPost={isNewPost} />
        <NewPost onNewPostHandler={newPostHandler} />
      </div>
    </section>
  );
};

export default Posts;
