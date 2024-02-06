import { useState, useEffect, Fragment, useContext } from "react";
import List from "@mui/joy/List";
import Sheet from "@mui/joy/Sheet";
import styles from "./AllUsers.module.css";
import SingleUser from "./SingleUser";
import LogRegisterContext from "../../contexts/log-register-context";
import SpinnerBox from "../Utils/SpinnerBox";
import fetchUrls from "../../helpers/fetch_urls";

const AllUsers = (props) => {
  const { token } = useContext(LogRegisterContext);
  const [allFetchedUsers, setAllFetchedUsers] = useState(null);
  const [isSendingRequest, setisSendingRequest] = useState(true);
  const [isUserDeleted, setIsUserDeleted] = useState(0);

  const userDeletedHandler = (username) => {
    console.log("test");
    fetch(`${fetchUrls["delete-user"]}/${username}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setIsUserDeleted((previousState) => previousState + 1);
      })
      .catch((error) => {
        console.log("test2");
        console.log(error);
      });
  };

  useEffect(() => {
    fetch(fetchUrls["get-all-users"], {
      headers: { Authorization: "Bearer " + token },
    })
      .then((response) => {
        console.log(response);
        return response.json();
      })
      .then((data) => {
        let users = [];
        for (let user of data.users) {
          users.push(user);
        }
        let sortedUsers = users.sort(function (a, b) {
          if (a.username < b.username) {
            return -1;
          }
          if (a.username > b.username) {
            return 1;
          }
          return 0;
        });
        setisSendingRequest(false);
        setAllFetchedUsers(sortedUsers);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [token, isUserDeleted]);

  return (
    <Fragment>
      <Sheet
        sx={{
          height: "85%",
          width: "75%",
          borderRadius: "md",
          overflow: "scroll",
          p: 2,
        }}
      >
        {isSendingRequest && <SpinnerBox />}
        {allFetchedUsers &&
          (allFetchedUsers.length === 0 ? (
            <p className={styles["no-posts-found"]}>No users created</p>
          ) : (
            <List
              sx={{
                width: "85%",
                m: "0 auto",
                p: 0,
                "--List-gap": "8px",
              }}
            >
              {allFetchedUsers.map((user) => (
                <SingleUser
                  key={user.email}
                  email={user.email}
                  username={user.username}
                  organizerRating={user.organizerRating}
                  userRating={user.userRating}
                  onDeleteHandler={userDeletedHandler}
                ></SingleUser>
              ))}
            </List>
          ))}
      </Sheet>
    </Fragment>
  );
};

export default AllUsers;
