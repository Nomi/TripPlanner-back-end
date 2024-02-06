import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import LogRegisterContext from "../../contexts/log-register-context";
import Home from "./Home";

function renderHomePage(context) {
  return render(
    <BrowserRouter>
      <LogRegisterContext.Provider value={context}>
        <Home isTest={true} />
      </LogRegisterContext.Provider>
    </BrowserRouter>
  );
}

describe("Home component", () => {
  test("renders its content correctly if the user logs in", () => {
    const contextValue = {
      token:
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ",
      firstLogin: false,
      login: (token) => {},
      logout: () => {},
      updateFirstLogin: () => {},
    };
    renderHomePage(contextValue);
    expect(screen.queryByRole("presentation")).not.toBeInTheDocument(); 
    expect(screen.queryByRole("button", {name: /^Close$/})).not.toBeInTheDocument();
    expect(screen.queryByRole("button", {name: /^Go to$/})).not.toBeInTheDocument();
  });

  test("renders its content correctly if the user logs in for the first time", () => {
    const contextValue = {
      token:
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ",
      firstLogin: true,
      login: (token) => {},
      logout: () => {},
      updateFirstLogin: () => {},
    };
    renderHomePage(contextValue);
    expect(screen.getByRole("presentation")).toBeInTheDocument(); 
    expect(screen.getByRole("button", {name: /^Close$/})).toBeInTheDocument();
    expect(screen.getByRole("button", {name: /^Go to$/})).toBeInTheDocument();
    expect(screen.getByRole("heading", {name: "First time on our page?"})).toBeInTheDocument();
  });
});
