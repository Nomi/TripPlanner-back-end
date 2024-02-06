import styles from "./Pages.module.css";
import UserProfile from '../components/UserProfile/UserProfile';

const ProfilePage = () => {
  return (
    <main className={styles["main-container"]}>
      <UserProfile />
    </main>
  );
};

export default ProfilePage;
