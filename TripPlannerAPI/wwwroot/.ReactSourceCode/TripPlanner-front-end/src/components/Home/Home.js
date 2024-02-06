import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import Sheet from "@mui/joy/Sheet";
import Typography from "@mui/joy/Typography";
import Button from "@mui/joy/Button";
import Modal from "@mui/joy/Modal";
import ModalClose from "@mui/joy/ModalClose";

import styles from "./Home.module.css";
import LogRegisterContext from "../../contexts/log-register-context";

const Home = (props) => {
  const logRegisterContext = useContext(LogRegisterContext);
  const navigate = useNavigate();

  const closeModal = () => {
    logRegisterContext.updateFirstLogin();
  };
  const closeModalRedirect = () => {
    logRegisterContext.updateFirstLogin();
    navigate("/help", { replace: true });
  };

  return (
    <section className={styles["home-page-container"]}>
      <Modal
        open={logRegisterContext.firstLogin}
        onClose={closeModal}
        sx={{ display: "flex", justifyContent: "center", alignItems: "center" }}
      >
        <Sheet
          variant="outlined"
          sx={{
            maxHeight: "80%",
            width: "25%",
            borderRadius: "md",
            p: 3,
            boxShadow: "lg",
          }}
        >
          <ModalClose
            variant="outlined"
            sx={{
              top: "calc(-1/4 * var(--IconButton-size))",
              right: "calc(-1/4 * var(--IconButton-size))",
              boxShadow: "0 2px 12px 0 rgba(0 0 0 / 0.2)",
              borderRadius: "50%",
              bgcolor: "white",
            }}
          />
          <Typography level="h6" sx={{ textAlign: "center", mb: 1 }}>
            First time on our page?
          </Typography>
          <Typography level="body1" sx={{ textAlign: "center", mb: 2 }}>
            Click Go to button to get to know more about the functionalities
          </Typography>
          <Sheet sx={{ display: "flex", justifyContent: "center" }}>
            <Button
              color="primary"
              variant="soft"
              onClick={closeModalRedirect}
              sx={{ mr: 1 }}
            >
              Go to
            </Button>
            <Button
              color="primary"
              variant="soft"
              onClick={closeModal}
              sx={{ ml: 1 }}
            >
              Close
            </Button>
          </Sheet>
        </Sheet>
      </Modal>
    </section>
  );
};

export default Home;
