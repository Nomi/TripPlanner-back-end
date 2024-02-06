import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import fetchUrls from "../../helpers/fetch_urls";
import styles from "./LogRegisterForm.module.css";
import LogRegisterContext from "../../contexts/log-register-context";
import TextField from "@mui/joy/TextField";
import Button from "@mui/joy/Button";
import Sheet from "@mui/joy/Sheet";
import Typography from "@mui/joy/Typography";
import LockRoundedIcon from "@mui/icons-material/LockRounded";
import PersonRoundedIcon from "@mui/icons-material/PersonRounded";

const LogRegisterForm = () => {
  const [isSendingRequest, setisSendingRequest] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const logRegisterContext = useContext(LogRegisterContext);

  const navigate = useNavigate();

  const usernameHandler = (event) => {
    setUsername(event.target.value);
  };

  const passwordHandler = (event) => {
    setPassword(event.target.value);
  };

  const toggleErrorMessage = () => {
    if (errorMessage) {
      setErrorMessage("");
    }
  };

  const submitHandler = async (event) => {
    event.preventDefault();
    setisSendingRequest(true);

    let url;
    let payload = {
      userName: username,
      password: password,
    };
    console.log(payload);
    url = fetchUrls.login;
    fetch(url, {
      method: "POST",
      body: JSON.stringify(payload),
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        console.log(response.status);
        setisSendingRequest(false);
        return response.json();
      })
      .then((data) => {
        console.log(data);
        data.token && logRegisterContext.setAdmin(data.token, true);
        data.token && navigate("/admin-panel", { replace: true });
      })
      .catch((error) => {
        setErrorMessage("Incorrect login or password");
        console.log(error);
      });
  };

  return (
    <section className={styles.forms}>
      <Sheet
        sx={{
          width: 280,
          mx: "auto",
          my: 4,
          py: 3,
          px: 2,
          display: "flex",
          flexDirection: "column",
          gap: 2,
          borderRadius: "sm",
          boxShadow: "md",
        }}
        variant="outlined"
      >
        <Typography level="h5" textAlign="center">
          Admin Login
        </Typography>
        <form
          onSubmit={submitHandler}
          style={{ display: "flex", flexDirection: "column", gap: "16px" }}
        >
          <TextField
            startDecorator={<PersonRoundedIcon />}
            placeholder="Username"
            type="text"
            required
            onChange={usernameHandler}
            error={
              errorMessage === "Username is already taken" ||
              errorMessage === "Incorrect login or password"
                ? true
                : false
            }
            helperText={
              errorMessage === "Username is already taken" ? errorMessage : ""
            }
            onFocus={toggleErrorMessage}
          />
          <TextField
            startDecorator={<LockRoundedIcon />}
            placeholder="Password"
            type="password"
            required
            onChange={passwordHandler}
            error={
              errorMessage ===
                "Password has to contain at least one digit, one capital ltter and one special character" ||
              errorMessage === "Incorrect login or password"
                ? true
                : false
            }
            helperText={
              errorMessage ===
                "Password has to contain at least one digit, one capital ltter and one special character" ||
              errorMessage === "Incorrect login or password"
                ? errorMessage
                : ""
            }
            onFocus={toggleErrorMessage}
          />
          <Button type="submit" loading={isSendingRequest} sx={{ mt: 1 }}>
            Log in
          </Button>
        </form>
      </Sheet>
    </section>
  );
};

export default LogRegisterForm;
