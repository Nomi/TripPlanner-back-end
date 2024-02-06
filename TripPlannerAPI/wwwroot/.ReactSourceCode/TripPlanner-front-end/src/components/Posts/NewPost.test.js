import { render, screen } from "@testing-library/react";
import NewPost from "./NewPost";

describe("NewPost component", () => {
  test("renders new post form correctly", () => {
    // Arrange
    render(<NewPost />);

    // Act
    // ... nothing

    // Assert

    expect(screen.getByRole("textbox")).toBeInTheDocument();
    expect(screen.getByRole("button")).toBeInTheDocument();
  });
});
