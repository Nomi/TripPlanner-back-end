import Slider from "@mui/joy/Slider";
import Sheet from "@mui/joy/Sheet";
import Typography from "@mui/joy/Typography";
import Button from "@mui/joy/Button";
import { Fragment, useState } from "react";

function Preferences(props) {
  let textWidth = 133;
  let alignText = "center";
  const [entertainment, setEntertainment] = useState(props.entertainment);
  const [sightseeing, setSightseeing] = useState(props.sightseeing);
  const [exploring, setExploring] = useState(props.exploring);
  const [culture, setCulture] = useState(props.culture);
  const [history, setHistory] = useState(props.history);
  const [freeride, setFreeride] = useState(props.freeride);
  const [training, setTraining] = useState(props.training);
  const [nature, setNature] = useState(props.nature);

  const entertainmentHandler = (event) => {
    setEntertainment(event.target.value);
  };

  const sightseeingHandler = (event) => {
    setSightseeing(event.target.value);
  };
  const exploringHandler = (event) => {
    setExploring(event.target.value);
  };
  const cultureHandler = (event) => {
    setCulture(event.target.value);
  };
  const historyHandler = (event) => {
    setHistory(event.target.value);
  };
  const freerideHandler = (event) => {
    setFreeride(event.target.value);
  };
  const trainingHandler = (event) => {
    setTraining(event.target.value);
  };
  const natureHandler = (event) => {
    setNature(event.target.value);
  };

  const savePreferences = () => {
    let preferences = {
      entertainment: entertainment,
      sightseeing: sightseeing,
      exploring: exploring,
      culture: culture,
      history: history,
      freeride: freeride,
      training: training,
      nature: nature
    }
    props.onSavePreferences(props.tripType, preferences);
  };

  return (
    <Fragment>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Entertainment</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={entertainment}
          onChange={entertainmentHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Sightseeing</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={sightseeing}
          onChange={sightseeingHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Exploring</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={exploring}
          onChange={exploringHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Culture</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={culture}
          onChange={cultureHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>History</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={history}
          onChange={historyHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Free ride</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={freeride}
          onChange={freerideHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Training</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={training}
          onChange={trainingHandler}
        />
      </Sheet>
      <Sheet sx={{ display: "flex", alignItems: "center", gap: 4, mb: 0.15 }}>
        <div style={{ width: textWidth, textAlign: alignText }}>
          <Typography sx={{ width: textWidth }}>Nature</Typography>
        </div>
        <Slider
          aria-label="Small steps"
          step={2}
          marks
          min={0}
          max={6}
          valueLabelDisplay="auto"
          value={nature}
          onChange={natureHandler}
        />
      </Sheet>
      <Button
        color="primary"
        variant="soft"
        sx={{ width: 100, mt: 2 }}
        onClick={savePreferences}
        loading={props.isSendingRequest}
      >
        Save
      </Button>
    </Fragment>
  );
}

export default Preferences;
