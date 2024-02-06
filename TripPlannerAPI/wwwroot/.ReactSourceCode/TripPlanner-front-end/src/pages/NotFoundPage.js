import styles from "./Pages.module.css";

const NotFoundPage = () => {
  return (
    <main className={styles["main-container"]}>
      <div className="centered">
        <p>Page not found!</p>
      </div>
    </main>
  );
};

export default NotFoundPage;
