import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import mockFetch from "../../mocks/mockFetch";
import LogRegisterContext from "../../contexts/log-register-context";
import MyTrips from "./MyTrips";

function renderMyTrips(context) {
  return render(
    <BrowserRouter>
      <LogRegisterContext.Provider value={context}>
        <MyTrips />
      </LogRegisterContext.Provider>
    </BrowserRouter>
  );
}

describe("MyTrips component", () => {
  beforeEach(() => {
    jest.spyOn(window, "fetch").mockImplementation(mockFetch);
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  test("renders my trips page correctly", async () => {
    // Arrange
    const contextValue = {
      token: "",
      firstLogin: false,
      login: (token) => {},
      logout: () => {},
      updateFirstLogin: () => {},
      joinedTrip: true,
    };
    renderMyTrips(contextValue);

    // Act
    // ... nothing

    // Assert
    const createdFutureButton = await screen.findByRole("tab", {
      name: /^Created future trips$/,
    });
    const joinedFutureButton = await screen.findByRole("tab", {
      name: /^Joined future trips$/,
    });
    const createdPastButton = await screen.findByRole("tab", {
      name: /^Created past trips$/,
    });
    const joinedPastButton = await screen.findByRole("tab", {
      name: /^Joined past trips$/,
    });
    expect(createdFutureButton).toBeInTheDocument();
    expect(joinedFutureButton).toBeInTheDocument();
    expect(createdPastButton).toBeInTheDocument();
    expect(joinedPastButton).toBeInTheDocument();
    expect(screen.getByRole("heading")).toHaveTextContent(
      "Browse through my trips"
    );
    expect(screen.getByTestId("date-type")).toHaveTextContent(
      "30.11.2022Car trip"
    );
    expect(screen.getByTestId("waypoints")).toHaveTextContent(
      "OlsztynWarszawa"
    );
    expect(
      screen.getByRole("link", { name: "Show details" })
    ).toBeInTheDocument();
  });
});
