import { toBeEnabled } from "@testing-library/jest-dom/dist/matchers";
import { render, screen } from "@testing-library/react";
import Map from "./MapReactLeaflet";

describe("Map component", () => {
  test("renders start and end fields as enabled in case of creating new trip", () => {
    // Arrange
    render(<Map userWaypointsInput={[]} staticMap={false} />);

    // Act
    // ... nothing

    // Assert
    expect(screen.getByPlaceholderText(/^start$/i)).toBeEnabled();
    expect(screen.getByPlaceholderText(/^end$/i)).toBeEnabled();
  });

  test("renders start and end fields as disable in case of browsing thorugh existing trips", () => {
    // Arrange
    render(<Map userWaypointsInput={[]} staticMap={true} />);

    // Act
    // ... nothing

    // Assert
    expect(screen.getByPlaceholderText(/^start$/i)).toBeVisible();
    expect(screen.getByPlaceholderText(/^end$/i)).toBeVisible();
  });
}); 
