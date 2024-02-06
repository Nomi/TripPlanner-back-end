import Button from "@mui/joy/Button";
import KeyboardArrowRight from "@mui/icons-material/KeyboardArrowRight";
import Select from "@mui/joy/Select";
import Option from "@mui/joy/Option";
import FormControl from "@mui/joy/FormControl";
import Input from "@mui/joy/Input";
import Typography from "@mui/joy/Typography";

import styles from "./DropdownList.module.css";

const DropdownList = (props) => {
  const typeDropDownHandler = (event, value) => {
    props.onTypeHandler(value);
  };

  const dateDropDownHandler = (event) => {
    props.onDateHandler(event.target.value);
  };

  const timeDropDownHandler = (event) => {
    props.onTimeHandler(event.target.value);
  };

  const submitHandler = (event) => {
    event.preventDefault();
    props.onSubmitFormHandler();
  };

  return (
    <div className={styles["new-trip-control"]}>
      <Typography level="h6" sx={{ mb: 2 }}>Design new trip</Typography>
      <form onSubmit={submitHandler}>
        <div className={styles["form-container"]}>
          <FormControl>
            <Input type="date" data-testid="date" onChange={dateDropDownHandler} />
          </FormControl>

          <FormControl sx={{ ml: 2 }}>
            <Input type="time" onChange={timeDropDownHandler} />
          </FormControl>

          <FormControl sx={{zIndex: 1000}}>
            <Select
              placeholder="Choose type"
              onChange={typeDropDownHandler}
              value={props.enteredType}
              sx={{ mx: 2}}
            >
              <Option value="car">Car trip</Option>
              <Option value="bike">Bike ride</Option>
              <Option value="foot">Hiking trip</Option>
            </Select>
          </FormControl>

          <Button
            color="primary"
            type="submit"
            disabled={!props.enableCreateButtonFlag}
            endDecorator={<KeyboardArrowRight />}
            loading={props.isSendingRequest}
          >
            Create
          </Button>
        </div>
      </form>
    </div>
  );
};

export default DropdownList;
