import React, { Suspense, useContext } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import SpinnerBox from "../Utils/SpinnerBox";

import LogRegisterContext from "../../contexts/log-register-context";
import LogRegisterPage from "../../pages/LogRegisterPage";
import HomePage from "../../pages/HomePage";
import NotFoundPage from "../../pages/NotFoundPage";
import NewTripPage from "../../pages/NewTripPage";
import AllTripsPage from "../../pages/AllTripsPage";
import TripDetailsAllPage from "../../pages/TripDetailsAllPage";
import MyTripsPage from "../../pages/MyTripsPage";
import TripDetailsMyPage from "../../pages/TripDetailsMyPage";
import ProfilePage from "../../pages/ProfilePage";
import HelpPage from "../../pages/HelpPage";
import PostsPage from "../../pages/PostsPage";
import LogRegisterAdminPage from "../../pages/LogRegisterAdminPage";
import FavoriteTripsPage from "../../pages/FavoriteTripsPage";
import TripDetailsFavoritePage from "../../pages/TripDetailsFavoritePage";
import AdminPanelPage from "../../pages/AdminPanelPage";
import LeaderboardPage from "../../pages/LeaderboardPage";

const MainContent = (props) => {
  const logRegisterContext = useContext(LogRegisterContext);

  return (
    <Suspense fallback={<div style={{marginTop: "2rem"}}><SpinnerBox /></div>}>
      <Routes>
        <Route path="/" element={<HomePage isTest={props.isTest} />} />
        <Route path="/admin" element={<LogRegisterAdminPage />} />
        <Route
          path="/admin-panel"
          element={
            logRegisterContext.token ? (
              <AdminPanelPage />
            ) : (
              <Navigate to="/admin" />
            )
          }
        />
        {!logRegisterContext.token && (
          <Route path="/auth" element={<LogRegisterPage />} />
        )}
        <Route
          path="/profile"
          element={
            logRegisterContext.token ? <ProfilePage /> : <Navigate to="/auth" />
          }
        />
        <Route
          path="/help"
          element={
            logRegisterContext.token ? <HelpPage /> : <Navigate to="/auth" />
          }
        />
        <Route
          path="/new-trip"
          element={
            logRegisterContext.token ? <NewTripPage /> : <Navigate to="/auth" />
          }
        />
        <Route
          path="/trips"
          element={
            logRegisterContext.token ? (
              <AllTripsPage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route
          path="/trips/:tripId"
          element={
            logRegisterContext.token ? (
              <TripDetailsAllPage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route
          path="/my-trips"
          element={
            logRegisterContext.token ? <MyTripsPage /> : <Navigate to="/auth" />
          }
        />
        <Route
          path="/my-trips/:tripId"
          element={
            logRegisterContext.token ? (
              <TripDetailsMyPage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route
          path="/my-trips/:tripId/chat"
          element={
            logRegisterContext.token ? <PostsPage /> : <Navigate to="/auth" />
          }
        />
        <Route
          path="/favorite-trips"
          element={
            logRegisterContext.token ? (
              <FavoriteTripsPage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route
          path="/favorite-trips/:tripId"
          element={
            logRegisterContext.token ? (
              <TripDetailsFavoritePage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route
          path="/leaderboard"
          element={
            logRegisterContext.token ? (
              <LeaderboardPage />
            ) : (
              <Navigate to="/auth" />
            )
          }
        />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </Suspense>
  );
};

export default MainContent;
