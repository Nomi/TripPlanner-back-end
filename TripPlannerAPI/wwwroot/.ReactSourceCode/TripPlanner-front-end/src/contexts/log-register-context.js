import React from "react";

const LogRegisterContext = React.createContext({
  token: "",
  firstLogin: true,
  login: (token) => {},
  logout: () => {},
  updateFirstLogin: () => {},
});

export default LogRegisterContext;
