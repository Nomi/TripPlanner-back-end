import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { BrowserRouter } from "react-router-dom";
import NewTripTest from "../Utils/Test/NewTripTest";

describe("NewTrip component", () => {
  test("renders create trip page correctly", () => {
    // Arrange
    render(
      <BrowserRouter>
        <NewTripTest />
      </BrowserRouter>
    );

    // Act
    // ... nothing

    // Assert
    expect(screen.getByTestId("date")).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /^Create$/ })).toBeDisabled();
    expect(screen.getByPlaceholderText(/^start$/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/^end$/i)).toBeInTheDocument();
  });

  test("activates 'Create' button after filling in all the fields", () => {
    // Arrange
    render(
      <BrowserRouter>
        <NewTripTest />
      </BrowserRouter>
    );

    // Act
    // ... nothing
    userEvent.type(screen.getByTestId("date"), new Date().toJSON());
    userEvent.type(screen.getByPlaceholderText(/^start$/i), "Warszawa");
    userEvent.keyboard("{enter}");
    userEvent.type(screen.getByPlaceholderText(/^end$/i), "Olsztyn");
    userEvent.keyboard("{enter}");
  

    // Assert
    expect(screen.getByPlaceholderText(/^start$/i)).toHaveDisplayValue(
      "Warszawa"
    );
    expect(screen.getByPlaceholderText(/^end$/i)).toHaveDisplayValue("Olsztyn");
    expect(screen.getByRole("button", { name: /^Create$/ })).toBeDisabled();
  });
});
