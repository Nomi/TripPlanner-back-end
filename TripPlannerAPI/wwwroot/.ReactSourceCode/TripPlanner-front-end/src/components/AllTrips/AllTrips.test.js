import { render, screen } from "@testing-library/react";
import mockFetch from "../../mocks/mockFetch";
import { BrowserRouter } from "react-router-dom";
import AllTrips from "./AllTrips";

describe("AllTrips component", () => {
  beforeEach(() => {
    jest.spyOn(window, "fetch").mockImplementation(mockFetch);
  });

  afterEach(() => {
    jest.restoreAllMocks();
  });

  test("renders all trips page correctly", async () => {
    // Arrange
    render(
      <BrowserRouter>
        <AllTrips />
      </BrowserRouter>
    );

    // Act
    // ... nothing

    // Assert
    const filterButton = await screen.findByRole("button", { name: /^Filter$/ });
    expect(filterButton).toBeEnabled();
    expect(screen.getByRole("heading")).toHaveTextContent("Browse through available trips");
    expect(screen.getByRole("button", { name: /^Reset$/ })).toBeEnabled();
    expect(screen.getByTestId("date-type")).toHaveTextContent("30.11.2022Car trip");
    expect(screen.getByTestId("waypoints")).toHaveTextContent("OlsztynWarszawa");
    expect(screen.getByRole("link", {name: "Show details"})).toBeInTheDocument();
  });
});
