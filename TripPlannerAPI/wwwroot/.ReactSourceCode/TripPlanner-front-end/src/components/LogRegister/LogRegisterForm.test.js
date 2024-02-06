import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { BrowserRouter } from "react-router-dom";
import LogRegisterForm from "./LogRegisterForm";

describe("LogRegisterForm component", () => {
  test("renders login form correctly", () => {
    // Arrange
    render(
      <BrowserRouter>
        <LogRegisterForm />
      </BrowserRouter>
    );

    // Act
    // ... nothing

    // Assert
    expect(screen.getByRole("heading")).toHaveTextContent(/^Sign in$/);
    expect(screen.getByPlaceholderText(/^username$/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/^password$/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /^Log in$/ })).toBeInTheDocument();
  });

  test("renders sing up form correctly", () => {
    // Arrange
    render(
      <BrowserRouter>
        <LogRegisterForm />
      </BrowserRouter>
    );

    // Act
    userEvent.click(screen.getByTestId("no-account"));

    // Assert
    expect(screen.getByRole("heading")).toHaveTextContent(/^Sign up$/);
    expect(screen.getByPlaceholderText(/^username$/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/^email$/i)).toBeInTheDocument();
    expect(screen.getByPlaceholderText(/^password$/i)).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: /^Create$/ })
    ).toBeInTheDocument();
  });
});
