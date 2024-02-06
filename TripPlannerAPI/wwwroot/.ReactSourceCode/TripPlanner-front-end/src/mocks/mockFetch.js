const myTripsList = {
  trips: [
    {
      tripId: "-NGTvWu_IyGeBtOr8bXX",
      date: "2022-11-30T00:00:00.000Z",
      preferences: ["Entertainment"],
      type: "car",
      waypoints: [
        {
          lat: 53.7767239,
          lng: 20.477780523409734,
          name: "Olsztyn, powiat olsztyński, województwo warmińsko-mazurskie, Polska",
        },
        {
          lat: 52.2319581,
          lng: 21.0067249,
          name: "Warszawa, województwo mazowieckie, Polska",
        },
      ],
    },
  ],
};

const postList = {
  posts: [
    {
      postId: "-NISNGPSehqRi7-uvD1U",
      creatorUsername: "johnsmith96",
      content: "There are many variations of passages of Lorem Ipsum available",
      creationDateTime: "2022-12-04T13:43:57.820Z",
    },
  ],
};

export default async function mockFetch(url) {
  switch (url) {
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Trip/my-trips/created_future":
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Trip/my-trips/created_past":
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Trip/my-trips/joined_future":
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Trip/my-trips/joined_past":
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Trip/all": {
      return {
        ok: true,
        status: 200,
        json: async () => myTripsList,
      };
    }
    case "https://tripplannerapi20221213230613.azurewebsites.net/api/Post/trip/1": {
      return {
        ok: true,
        status: 200,
        json: async () => postList,
      };
    }
    default: {
      throw new Error(`Unhandled request: ${url}`);
    }
  }
}
