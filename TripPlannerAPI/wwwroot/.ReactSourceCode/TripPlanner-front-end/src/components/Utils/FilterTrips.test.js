import { render, screen } from "@testing-library/react";
import FilterTrips from "./FilterTrips";

describe("FilterTrips component", () => {
  test("renders filter dropdowns correctly", () => {
    // Arrange
    render(<FilterTrips />); 

    // Act
    // ... nothing

    // Assert
    expect(screen.getByRole("button", { name: /^Filter$/ })).toBeEnabled();
    expect(screen.getByRole("button", { name: /^Reset$/ })).toBeEnabled();
  });
});
