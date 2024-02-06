import { render, screen } from "@testing-library/react";
import DropdownList from "./DropdownList";

describe("DropdownList component", () => {
  test("renders list of dropdowns correctly", () => {
    // Arrange
    render(<DropdownList />);

    // Act
    // ... nothing

    // Assert
    expect(screen.getByRole("button", { name: /^Create$/ })).toBeDisabled();
  });
});
