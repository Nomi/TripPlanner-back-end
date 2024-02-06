import { render, screen } from "@testing-library/react";
import SinglePost from "./SinglePost";

describe("SinglePost component", () => {
  test("renders single post correctly", () => {
    // Arrange
    render(
      <SinglePost
        author="johnsmith96"
        content="There are many variations of passages of Lorem Ipsum available"
        day="4"
        month="December"
        year="2022"
      />
    );

    // Act
    // ... nothing

    // Assert
    expect(screen.getByTestId("date")).toHaveTextContent("4 December 2022");
    expect(screen.getByTestId("content")).toHaveTextContent(
      `There are many variations of passages of Lorem Ipsum available`
    );
    expect(screen.getByTestId("author")).toHaveTextContent("johnsmith96");
  });
});
