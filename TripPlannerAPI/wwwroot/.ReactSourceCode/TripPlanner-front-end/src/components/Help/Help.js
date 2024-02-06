import Accordion from "react-bootstrap/Accordion";


const Help = () => {
  return (
    <section>
      <div>
        <Accordion alwaysOpen>
          <Accordion.Item eventKey="0">
            <Accordion.Header>How to create trip?</Accordion.Header>
            <Accordion.Body>
              <p>In order to create a trip you have to:</p>
              <ul>
                <li>Click on the Create Trip Tab</li>
                <li>Fill in Trip Date, Start time, Type (default car trip), Description fields by using chosen values from the dropdowns</li>
                <li>Choose tags that most accurately describe your trip</li>
                <li>Fill in Start and End fields in the right-top corner of the map (you have to press Enter after filling in every field)</li>
                <li>
                  If you want to create a trip consisting of more than 2 waypoints you can add new ones by clicking plus button. Keep in mind that the
                  bottom-most element is your endpoint
                </li>
                <li>Having filled in all the fields Create button will become enabled. Click it if you want to create new trip</li>
              </ul>
            </Accordion.Body>
          </Accordion.Item>
          <Accordion.Item eventKey="1">
            <Accordion.Header>How to join trip?</Accordion.Header>
            <Accordion.Body>
              <p>In order to join a trip you have to:</p>
              <ul>
                <li>Click on the All Trips Tab</li>
                <li>Choose the trip you are interested in joining to</li>
                <li>If you want to restrict the number of trips you can use filtering options provided by dropdowns on the top</li>
                <li>Click Show Details button</li>
                <li>On the new page you will see more details regarding the trip</li>
                <li>By clicking Show Participants button you can view who is gonna be one the trip with you</li>
                <li>Cick Join trip button should you make up your mind</li>
              </ul>
            </Accordion.Body>
          </Accordion.Item>
          <Accordion.Item eventKey="2">
            <Accordion.Header>How to access private communication group?</Accordion.Header>
            <Accordion.Body>
              <p>In order to access a private communication group trip you have to:</p>
              <ul>
                <li>Click on the My Trips Tab</li>
                <li>Choose the trip for which you want to access communications group</li>
                <li>You can switch between your created and joined trips (past and future) by using the tabs on the top</li>
                <li>Click Show Details button</li>
                <li>On the new page you will see more details regarding the trip</li>
                <li>By clicking Open chat button you can access private communication group page</li>
              </ul>
            </Accordion.Body>
          </Accordion.Item>
        </Accordion>
      </div>
    </section>
  );
};

export default Help;
