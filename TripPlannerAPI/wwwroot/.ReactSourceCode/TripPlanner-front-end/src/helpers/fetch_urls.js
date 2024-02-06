//const backendBaseUrl = "http://127.0.0.1:7034";
const backendBaseUrl = ""; //for static build files to be deployed directly from the backend server.

const fetchUrls = {
  login:
    backendBaseUrl+"/api/Account/login",
  register:
    backendBaseUrl+"/api/Account/register",
  "current-user":
    backendBaseUrl+"/api/Account/current_user",
  "get-all-users":
    backendBaseUrl+"/api/Account/all-users",
  "delete-user":
    backendBaseUrl+"/api/Account/user",
  "create-trip":
    backendBaseUrl+"/api/Trip/new",
  "get-all-trips":
    backendBaseUrl+"/api/Trip/all",
  "get-my-trips":
    backendBaseUrl+"/api/Trip/my-trips",
  "get-favorite-trips":
    backendBaseUrl+"/my-favorites/all",
  "add-favorite-trips":
    backendBaseUrl+"/my-favorites/add",
  rating:
    backendBaseUrl+"/api/UserRating",
  preferences:
    backendBaseUrl+"/api/TripTypePreference",
  posts: backendBaseUrl+"/api/Post/trip",
  pins: backendBaseUrl+"/pins",
  leaderboard: backendBaseUrl+"",
};

export default fetchUrls;
