using System;
using System.Collections.ObjectModel;
using LogicLayer;

namespace ModelLayer
{
    public class Model
    {
        private readonly AbstractLogicAPI logicAPI;
        private ObservableCollection<BallAdapter> observableBallCollection = new ObservableCollection<BallAdapter>();
        public ObservableCollection<BallAdapter> ObservableBallCollection
        {
            get => observableBallCollection;
            set => observableBallCollection = value;
        }

        public Model(AbstractLogicAPI logicAPI = null)
        {
            this.logicAPI = (logicAPI == null) ? AbstractLogicAPI.createLogicAPI() : logicAPI;

            this.logicAPI.createBalls(2);
        }

        public void start()
        {
            logicAPI.start();
            foreach (Ball b in logicAPI.GetBalls())
            {
                ObservableBallCollection.Add(new BallAdapter(b));
            }
        }

        public void stop()
        {
            logicAPI.stop();
        }

        public void addBall()
        {
            logicAPI.createBall();
            observableBallCollection.Add(new BallAdapter(logicAPI.GetBalls()[logicAPI.GetBalls().Count - 1]));
        }

        public void removeBall()
        {
            logicAPI.deleteBall();
            observableBallCollection.RemoveAt(observableBallCollection.Count - 1);
        }
    }

}
