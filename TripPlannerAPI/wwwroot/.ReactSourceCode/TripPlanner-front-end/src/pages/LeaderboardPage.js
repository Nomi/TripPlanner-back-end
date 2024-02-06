import styles from "./Pages.module.css";
import Leaderboard from "../components/Leaderboard/Leaderboard";

const LeaderboadPage = () => {
  return (
    <main className={styles["main-container"]}>
      <Leaderboard />
    </main>
  );
};

export default LeaderboadPage; 
 