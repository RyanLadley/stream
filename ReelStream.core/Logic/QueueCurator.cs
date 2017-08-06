using ReelStream.core.Models.Buisness;
using ReelStream.data.Models.Entities;
using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Logic
{
    public class QueueCurator
    {
        private IMovieRepository _movieRepository;

        enum PredefinedQueue
        {
            CONTINUE_WATCHING,
            NEWLY_ADDED
        }

        public QueueCurator(IMovieRepository movieRepo)
        {
            _movieRepository = movieRepo;
        }

        public List<MovieQueue> BuildMovieQueues(long userId)
        {
            List<MovieQueue> queues = new List<MovieQueue>();

            //Loop through all predfined queues and add them if values are returned 
            foreach(PredefinedQueue predefined in Enum.GetValues(typeof(PredefinedQueue)))
            {
                if (_buildPredefinedQueue(userId, predefined, out MovieQueue queue))
                    queues.Add(queue);
            }

            return queues;
        }


        private bool _buildPredefinedQueue(long userId, PredefinedQueue predefined, out MovieQueue queue)
        {
            bool queueBuilt = false;
            queue = new MovieQueue();

            var movies = _getQueueMovies(userId, predefined);
            if (movies != null && movies.Count != 0)
            {
                queue.Name = _getPredefinedQueueName(predefined);
                queue.Movies = movies;

                queueBuilt = true;
            }

            return queueBuilt;
        }

        private List<Movie> _getQueueMovies(long userId, PredefinedQueue predefined)
        {
            //This could probably be combined with the _getPredefinedQueueName bellow
            switch (predefined)
            {
                case PredefinedQueue.NEWLY_ADDED:
                    return _movieRepository.GetNewlyAddedMovies(userId);

                case PredefinedQueue.CONTINUE_WATCHING:
                    return _movieRepository.GetMoviesInProgress(userId);

                default:
                    return null;
            }
        }
        
        private string _getPredefinedQueueName(PredefinedQueue predefined)
        {
            switch (predefined)
            {
                case PredefinedQueue.NEWLY_ADDED:
                    return "Newly Added Movies";

                case PredefinedQueue.CONTINUE_WATCHING:
                    return "Continue Watching";

                default:
                    return "Movies";
            }
        }
    }
}
